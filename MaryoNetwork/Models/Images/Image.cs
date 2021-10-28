using MaryoNetwork.Models.Posts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MaryoNetwork.Models.Images
{
    public class Image
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string FileType { get; set; }
        public string Extension { get; set; }
        public string Description { get; set; }
        public string UploadedById { get; set; }
        public User UploadedBy { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }
        public DateTime? CreatedOn { get; set; }
        public byte[] Data { get; set; }
    }
}
