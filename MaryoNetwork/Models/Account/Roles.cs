using System.ComponentModel.DataAnnotations;

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
