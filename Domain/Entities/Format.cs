using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Domain.Entities
{
    public class Format
    {
        [HiddenInput(DisplayValue = false)]
        [Display(Name = "ID")]
        public int FormatId { get; set; }

        [Display(Name = "Назва")]
        [Required(ErrorMessage = "Будь ласка, введіть назву формату")]
        public string FormatName { get; set; }

        [HiddenInput(DisplayValue = false)]
        [Display(Name = "Тури")]
        public List<Tour> Tours { get; set; }
    }
}
