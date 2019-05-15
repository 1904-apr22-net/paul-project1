using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplicationWeb.Models
{
    public class ModelInventories
    {
        [Required]
        [Display(Name = "Inventory ID")]
        public int InventoryId { get; set; }
        [Required]
        [Display(Name = "Store ID")]
        public int StoreId { get; set; }
        [Required]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }
        [Required]
        [Display(Name = "Quantity")]
        public int Quantity { get; set; }
        [Required]
        [Display(Name = "Product")]
        public ModelProduct Product { get; set; }
        [Required]
        [Display(Name = "Store")]
        public ModelLocation Store { get; set; }
    }
}
