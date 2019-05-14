using Microsoft.AspNetCore.Mvc.ModelBinding;
using StoreApplication.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplicationWeb.Models
{
    public class ModelCustomer
    {
        [Required]
        [Display(Name = "Store ID")]
        public int StoreId { get; set; }
        [Required]
        [Display(Name = "First Name")]
        public string FName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LName { get; set; }
        [Required]
        [Display(Name = "State")]
        public string State { get; set; }
        [Required]
        [Display(Name = "Customer ID")]
        public int CustomerId { get; set; }
        [Required]
        [Display(Name = "Store")]
        public Location DefaultLocation { get; set; }
        [Required]
        [Display(Name = "Orders")]
        public IList<Order> Orders { get; set; } = new List<Order>();


    }
}
