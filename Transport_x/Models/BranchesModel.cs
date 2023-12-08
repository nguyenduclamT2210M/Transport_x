using System.ComponentModel.DataAnnotations;

namespace Transport_x.Models
{
    public class BranchesModel
    {
        public int IdBranches { get; set; }
        public string NameBranches { get; set; }
        public string AddressBranches { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string TelephoneBranches { get; set; }
    }
}
