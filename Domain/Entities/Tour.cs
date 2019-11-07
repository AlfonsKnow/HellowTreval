using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Tour
    {
        public int TourId { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int FormatId { get; set; }
        public Format Format { get; set; }
        public decimal Price { get; set; }
    }
}
