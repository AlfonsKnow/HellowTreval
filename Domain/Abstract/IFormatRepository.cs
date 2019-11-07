using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IFormatRepository
    {
        void SaveFormat(Format format);
        void RemoveFormat(Format format);
        IEnumerable<Format> GetAllFormats();
    }
}
