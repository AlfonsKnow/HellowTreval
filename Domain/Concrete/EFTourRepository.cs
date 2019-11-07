using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFTourRepository : ITourRepository
    {
        EFDbContext context = new EFDbContext();

        public IEnumerable<Tour> GetAllTours()
        {
            return context.Tours.Include("Format").ToList();
        }

        public void SaveTour(Tour tour)
        {
            if (tour.TourId == 0)
            {
                context.Tours.Add(tour);
            }
            else
            {
                Tour dbEntry = context.Tours.Find(tour.TourId);
                if (dbEntry != null)
                {
                    dbEntry.Type = tour.Type;
                    dbEntry.Price = tour.Price;
                    dbEntry.Description = tour.Description;
                    dbEntry.Name = tour.Name;
                    dbEntry.FormatId = tour.FormatId;
                    dbEntry.Image = tour.Image;
                }
            }
            context.SaveChanges();
        }

        public void RemoveTour(Tour tour)
        {
            context.Tours.Attach(tour);
            context.Tours.Remove(tour);

            context.SaveChanges();
        }
    }
}
