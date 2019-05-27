using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StoreApplicationWeb.Models
{
    public class ModelComponentInv
    {
        [Required]
        [Display(Name = "Component ID")]
        public int ComponentId { get; set; }
        [Required]
        [Display(Name = "Base Product ID")]
        public int BaseProductId { get; set; }
        [Required]
        [Display(Name = "Component ID")]
        public int ComponentProductId { get; set; }
        [Required]
        [Display(Name = "Base Product")]
        public ModelProduct BaseProduct { get; set; }
        [Required]
        [Display(Name = "Components")]
        public ModelProduct ComponentProduct { get; set; }
    }
}
