using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Models.Account
{
    public class Roles
    {
        [Display(Name = "Role")]
        public string RoleId { get; set; }
        [Display(Name = "User")]
        public string UserId { get; set; }
    }
}
