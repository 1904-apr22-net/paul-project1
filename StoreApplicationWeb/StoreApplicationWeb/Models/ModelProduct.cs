using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplicationWeb.Models
{
    public class ModelProduct
    {
        [Required]
        [Display(Name = "Price")]
        public decimal ProductCost { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ProductName { get; set; }

        [Required]
        [Display(Name = "Product ID")]
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Amount")]
        public int quantitySale { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string ProductDesc { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "Category ID")]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Components")]
        public bool HasComponents { get; set; }

        [Required]
        [Display(Name = "Category")]
        public ModelProductCat ProductCat { get; set; }

        [Required]
        [Display(Name = "Base Product")]
        public IList<ModelComponentInv> BaseComponents { get; set; } = new List<ModelComponentInv>();
        //public IList<Inventories> Inventory { get; set; } = new List<Inventories>();
        [Required]
        [Display(Name = "Components")]
        public IList<ModelComponentInv> Components { get; set; } = new List<ModelComponentInv>();
        [Required]
        [Display(Name = "Inventory")]
        public IList<ModelInventories> Inventory { get; set; } = new List<ModelInventories>();
        [Required]
        [Display(Name = "Order Details")]
        public IList<ModelOrderDetails> OrderDetails { get; set; }
    }
}
