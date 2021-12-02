using System.ComponentModel.DataAnnotations.Schema;

namespace MaryoNetwork.Models.Editors
{
    public class Favorite
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string EditorId { get; set; }
        public Editor Editor { get; set; }
    }
}
