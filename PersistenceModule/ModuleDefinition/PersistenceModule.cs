using Interfaces.PersisenceModule.Datamodule;
using Interfaces.PersisenceModule.Repositories;
using Interfaces.PersisenceModule.Services;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using PersistenceModule.Api;
using PersistenceModule.Data.Datamodules;
using PersistenceModule.Data.Repositories;
using PersistenceModule.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceModule.ModuleDefinition
{
    public class PersistenceModule : IModule
    {
        IUnityContainer UnityContainer { get; set; }
        IRegionManager RegionManager { get; set; }

        public PersistenceModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            UnityContainer = unityContainer;
            RegionManager = regionManager;
        }


        public void Initialize()
        {
            RegisterServices();
            RegisterViews();
            RegisterEvents();
        }

        void RegisterServices()
        {
            // add Services
            // IMyService myService = ServiceLocator.GetInstance<IMyService>();
            // or

            UnityContainer.RegisterInstance<AntelopeRestApi>(new AntelopeRestApi(UnityContainer.Resolve<IEventAggregator>()));

            UnityContainer.RegisterType<ILocation, Location>();
            UnityContainer.RegisterType<ILocationRepository, LocationRepository>();

            UnityContainer.RegisterType<INation, Nation>();
            UnityContainer.RegisterType<INationRepository, NationRepository>();

            UnityContainer.RegisterType<IChampionship, Championship>();
            UnityContainer.RegisterType<IChampionshipRepository, ChampionshipRepository>();

            UnityContainer.RegisterType<IRepositoryService, RepositoryService>();
        }

        void RegisterViews()
        {
            // add Views to regions
            // RegionManager.RegisterViewWithRegion("regionName", typeof(MyUSerControl));
        }

        void RegisterEvents()
        {
            // register Events to the eventaggregator 
        }
    }
}
