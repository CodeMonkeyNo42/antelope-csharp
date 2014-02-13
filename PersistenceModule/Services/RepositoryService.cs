using Interfaces.PersisenceModule.Repositories;
using Interfaces.PersisenceModule.Services;
using Microsoft.Practices.Unity;
using PersistenceModule.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.Services
{
    class RepositoryService : IRepositoryService
    {
        IUnityContainer UnityContainer { get; set; }

        public RepositoryService(IUnityContainer unityContainer)
        {
            UnityContainer = unityContainer;
        }

        private ILocationRepository locationRepository;
        public ILocationRepository LocationRepository
        {
            get
            {
                if (locationRepository == null)
                {
                    locationRepository = UnityContainer.Resolve<ILocationRepository>();
                }
                return locationRepository;
            }
        }
    }
}
