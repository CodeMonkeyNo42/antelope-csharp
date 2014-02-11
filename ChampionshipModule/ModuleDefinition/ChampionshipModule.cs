using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChampionshipModule.ModuleDefinition
{
    public class ChampionshipModule : IModule
    {
        IUnityContainer UnityContainer { get; set; }
        IRegionManager RegionManager { get; set; }

        public ChampionshipModule(IUnityContainer unityContainer, IRegionManager regionManager)
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
            // UnityContainer.RegisterType<IMyService, MyService>();
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
