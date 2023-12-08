using System.ComponentModel.DataAnnotations;

namespace Transport_x.DTOs
{
    public class UserDTO
    {
        public int IdUser { get; set; }
        public string FullNameUser { get; set; }
        public string EmailUser { get; set; }
        public string TelephoneUser { get; set; }
        public string PassWordUser { get; set; }
        public string AddressUser { get; set; }
    }
}
