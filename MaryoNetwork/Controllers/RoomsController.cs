﻿using AutoMapper;
using MaryoNetwork.Data;
using MaryoNetwork.Hubs;
using MaryoNetwork.Models.Messenger;
using MaryoNetwork.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IHubContext<ChatHub> _hubContext;

        public RoomsController(ApplicationDbContext db,
            IMapper mapper,
            IHubContext<ChatHub> hubContext)
        {
            _db = db;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoomViewModel>>> Get()
        {
            var rooms = await _db.Rooms.ToListAsync();

            var roomsViewModel = _mapper.Map<IEnumerable<Room>, IEnumerable<RoomViewModel>>(rooms);

            return Ok(roomsViewModel);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Room>> Get(string id)
        {
            var room = await _db.Rooms.FindAsync(id);
            if (room == null)
                return NotFound();

            var roomViewModel = _mapper.Map<Room, RoomViewModel>(room);
            return Ok(roomViewModel);
        }

        [HttpPost]
        public async Task<ActionResult<Room>> Create(RoomViewModel roomViewModel)
        {
            if (_db.Rooms.Any(r => r.Name == roomViewModel.Name))
                return BadRequest("Invalid room name or room already exists");

            var user = _db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
            var room = new Room()
            {
                Name = roomViewModel.Name,
                Admin = user
            };

            _db.Rooms.Add(room);
            await _db.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("addChatRoom", new { id = room.Id, name = room.Name });

            return CreatedAtAction(nameof(Get), new { id = room.Id }, new { id = room.Id, name = room.Name });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(string id, RoomViewModel roomViewModel)
        {
            if (_db.Rooms.Any(r => r.Name == roomViewModel.Name))
                return BadRequest("Invalid room name or room already exists");

            var room = await _db.Rooms
                .Include(r => r.Admin)
                .Where(r => r.Id == id && r.Admin.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (room == null)
                return NotFound();

            room.Name = roomViewModel.Name;
            await _db.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("updateChatRoom", new { id = room.Id, room.Name });

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var room = await _db.Rooms
                .Include(r => r.Admin)
                .Where(r => r.Id == id && r.Admin.UserName == User.Identity.Name)
                .FirstOrDefaultAsync();

            if (room == null)
                return NotFound();

            _db.Rooms.Remove(room);
            await _db.SaveChangesAsync();

            await _hubContext.Clients.All.SendAsync("removeChatRoom", room.Id);
            await _hubContext.Clients.Group(room.Name).SendAsync("onRoomDeleted", string.Format("Room {0} has been deleted.\nYou are moved to the first available room!", room.Name));

            return NoContent();
        }
    }
}
