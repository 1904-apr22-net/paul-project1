using Microsoft.AspNetCore.Mvc.Rendering;
using StoreApplication.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;


namespace StoreApplicationWeb.Models
{
    public class ModelOrder
    {
        [Required]
        [Display(Name = "Order ID")]
        public int OrderId { get; set; }
        public string error { get; set; }

        [Required]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }

        [Required]
        [Display(Name = "Store ID")]
        public int StoreId { get; set; }

        [Required]
        public IList<Product> Products { get; set; }

        [Required]
        [Display(Name = "Store")]
        public Location Location { get; set; }

        [Required]
        [Display(Name = "Stores")]
        public List<Location> LocationList { get; set; }
        [Required]
        [Display(Name = "Customer Selection")]
        public List<SelectListItem> chooseCust = new List<SelectListItem>();
        [Required]
        [Display(Name = "Product Selection")]
        public List<SelectListItem> chooseProd = new List<SelectListItem>();
        [Required]
        [Display(Name = "Customer")]
        public Customer Customer { get; set; }

        [Required]
        [Display(Name = "Time")]
        public DateTime TimeStamp { get; set; }

        [Required]
        [Display(Name = "Total Cost")]
        public Decimal TotalAmount { get; set; }

        [Required]
        [Display(Name = "Order Details")]
        public IList<OrderDetails> OrderDetails { get; set; } = new List<OrderDetails>();
    }
}
