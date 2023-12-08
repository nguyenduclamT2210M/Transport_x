using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Transport_x.Entities;

namespace Transport_x.Models
{
    public class EmployeeModel
    {
        public int IdEmployee { get; set; }
        public string UserName { get; set; }
        public int IdBranches { get; set; }
        [ForeignKey("IdBranches")]
        public Branches Branches { get; set; }
        public string FullName { get; set; }
        [RegularExpression(@"^\w+([\.-]?\w+)*@gmail\.com$", ErrorMessage = "Invalid Gmail address.")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string role {get; set;}
    }
}
