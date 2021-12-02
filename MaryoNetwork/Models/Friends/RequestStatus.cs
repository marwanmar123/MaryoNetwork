using System.ComponentModel.DataAnnotations.Schema;

namespace MaryoNetwork.Models.Friends
{
    public class RequestStatus
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Status { get; set; }
    }
}
