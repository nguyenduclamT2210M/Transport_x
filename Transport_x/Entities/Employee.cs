using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transport_x.Entities
{
    public class Employee
    {
        [Key]
        public int IdEmployee { get; set; }
        public string UserName { get; set; }
        public int IdBranches { get; set; }
        [ForeignKey("IdBranches")]
        public Branches Branches { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string role { get; set; }
    }
}
