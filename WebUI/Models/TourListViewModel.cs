using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class TourListViewModel
    {
        public IEnumerable<Tour> Tours { get; set; }
        public IEnumerable<Format> Formats { get; set; }
        public IEnumerable<string> Types { get; set; }
        public int CurentFormat { get; set; }
        public string CurentType { get; set; }
    }
}