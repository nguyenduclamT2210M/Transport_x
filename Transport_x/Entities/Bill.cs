﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transport_x.Entities
{
    [Table("Bills")]
    public class Bill
    {
        [Key]
        public int IdBill { get; set; }
        public int IdUser { get; set; }
        [ForeignKey("IdUser")]
        public User User { get; set; }
        public int IdShippingType { get; set; }
        [ForeignKey("IdShippingType")]
        public ShippingType ShippingType { get; set; }
        public string ConsigneeName { get; set; }
        public string ConsigneeAddress { get; set; }
        [Required]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be 10 digits.")]
        public string ConsigneeTel { get; set; }
        public decimal Change { get; set; }
        public string PickUp { get; set; }
        public string Cod { set; get; }
        public string Payer { get; set;}
    }
}
