using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplicationWeb.Models
{
    public class ModelLocation
    {
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        [Display(Name = "Location ID")]
        public int LocationId { get; set; }
        [Required]
        [Display(Name = "Orders")]
        public IList<ModelOrder> Orders { get; set; } = new List<ModelOrder>();
        [Required]
        [Display(Name = "Customers")]
        public IList<ModelCustomer> Customers { get; set; } = new List<ModelCustomer>();
        [Required]
        [Display(Name = "Inventory")]
        public IList<ModelInventories> Inventory { get; set; } = new List<ModelInventories>();

    }
}
