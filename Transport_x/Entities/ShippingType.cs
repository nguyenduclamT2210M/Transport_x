using System.ComponentModel.DataAnnotations;

namespace Transport_x.Entities
{
    public class ShippingType
    {
        [Key]
        public int IdShipType { get; set; }
        public string NameShip { get; set; }
        public decimal ChageRate { get; set; }
    }
}
