using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplicationWeb.Models
{
    public class ModelProductCat
    {
        [Required]
        [Display(Name = "Category ID")]
        public int ProductCategoryId { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string CategoryName { get; set; }

        [Required]
        [Display(Name = "Products")]
        public List<ModelProduct> Products { get; set; }
    }
}
