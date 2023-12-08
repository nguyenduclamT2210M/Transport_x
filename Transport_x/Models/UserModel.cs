using System.ComponentModel.DataAnnotations;

namespace Transport_x.Models
{
    public class UserModel
    {
        [Key]
        public int IdUser { get; set; }
        public string FullNameUser { get; set; }
        [RegularExpression(@"^\w+([\.-]?\w+)*@gmail\.com$", ErrorMessage = "Invalid Gmail address.")]
        public string EmailUser { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string TelephoneUser { get; set; }
        public string PassWordUser { get; set; }
        public string AddressUser { get; set; }
    }
}
