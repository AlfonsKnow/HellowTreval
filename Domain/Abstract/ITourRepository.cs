using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface ITourRepository
    {
        void SaveTour(Tour tour);
        void RemoveTour(Tour tour);
        IEnumerable<Tour> GetAllTours();
    }
}
