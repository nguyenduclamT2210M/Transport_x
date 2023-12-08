using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transport_x.Entities
{
    public class Status
    {
        [Key]
        public int IdStatus { get; set; }
        public string TypeStatus { get; set; }
        public int IdEmployee { get; set; }
        [ForeignKey("IdEmployee")]
        public Employee Employee { get; set; }
        public DateTime StatusTime { get; set; }
        public int IdBill { get; set; }
        [ForeignKey("IdBill")]
        public Bill Bill { get; set; }
    }
}
