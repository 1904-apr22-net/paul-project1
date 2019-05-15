using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplicationWeb.Models
{
    public class ModelOrderDetails
    {
        [Required]
        [Display(Name = "Order Item ID")]
        public int OrderItemId { get; set; }
        [Required]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "Order")]
        public ModelOrder Order { get; set; }
        [Required]
        [Display(Name = "Product")]
        public ModelProduct Product { get; set; }
    }
}
