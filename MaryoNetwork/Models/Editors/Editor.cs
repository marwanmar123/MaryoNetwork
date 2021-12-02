using System.ComponentModel.DataAnnotations.Schema;

namespace MaryoNetwork.Models.Editors
{
    public class Editor
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Title { get; set; }
        public byte[] Image { get; set; }
        public string Html { get; set; }
        public string Css { get; set; }
        public string Js { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public Favorite Favorite { get; set; }
    }
}
