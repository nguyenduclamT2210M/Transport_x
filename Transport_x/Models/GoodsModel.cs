using System.ComponentModel.DataAnnotations.Schema;
using Transport_x.Entities;

namespace Transport_x.Models
{
    public class GoodsModel
    {
        public int IdGoods { get; set; }
        public string Name { get; set; }
        public int IdBill { get; set; }
      
        public Bill Bill { get; set; }
        public string Quantity { get; set; }
        public decimal Weight { get; set; }
        public decimal Valuse { get; set; }
        public string Nature { get; set; }
    }
}
