using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class TourEditViewModel
    {
        public Tour Tour { get; set; }
        public IEnumerable<Format> Formats { get; set; }
        public int CurentFormat { get; set; }
    }
}