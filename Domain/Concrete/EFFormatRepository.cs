using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFFormatRepository : IFormatRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Format> GetAllFormats()
        {
            return context.Formats.Include("Tours").ToList();
        }

        public void SaveFormat(Format format)
        {
            if (format.FormatId == 0)
            {
                context.Formats.Add(format);
            }
            else
            {
                Format dbEntry = context.Formats.Find(format.FormatId);
                if (dbEntry != null)
                {
                    dbEntry.FormatName = format.FormatName;
                }
            }
            context.SaveChanges();
        }

        public void RemoveFormat(Format format)
        {
            context.Formats.Attach(format);
            context.Formats.Remove(format);

            context.SaveChanges();
        }
    }
}
