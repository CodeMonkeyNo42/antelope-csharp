using PersistenceModule.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Services
{
    class LocationService
    {
        private LocationRepository locationRepository;
        public LocationRepository LocationRepository
        {
            get
            {
                if (locationRepository == null)
                {
                    locationRepository = new LocationRepository();
                }
                return locationRepository;
            }
        }
    }
}
