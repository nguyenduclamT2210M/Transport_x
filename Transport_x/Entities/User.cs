using System.ComponentModel.DataAnnotations;

namespace Transport_x.Entities
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        public string FullNameUser { get; set; }
        public string EmailUser { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string TelephoneUser { get; set; }
        public string PassWordUser { get; set; }
        public string AddressUser { get; set; }
    }
}
