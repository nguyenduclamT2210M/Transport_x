using System.ComponentModel.DataAnnotations.Schema;
using Transport_x.Entities;

namespace Transport_x.Models
{
    public class StatusModel
    {
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
