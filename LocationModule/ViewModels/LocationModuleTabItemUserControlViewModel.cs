using Interfaces.Events;
using Interfaces.PersisenceModule.Datamodule;
using Interfaces.PersisenceModule.Services;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocationModule.ViewModels
{
    public class LocationModuleTabItemUserControlViewModel : NotificationObject
    {
        public LocationModuleTabItemUserControlViewModel(IRepositoryService repositoryService, IEventAggregator eventAggregator, IUnityContainer unityContainer)
        {
            RepositoryService = repositoryService;
            EventAggregator = eventAggregator;
            UnityContainer = unityContainer;

            EventAggregator.GetEvent<RefreshViewsEvent>().Subscribe(Refresh);
        }

        private IEventAggregator EventAggregator { get; set; }
        private IRepositoryService RepositoryService { get; set; }
        private IUnityContainer UnityContainer { get; set; }

        private ObservableCollection<ILocation> locations;
        public ObservableCollection<ILocation> Locations 
        {
            get
            {
                return locations;
            }
            set
            {
                if (locations == null)
                {
                    locations = RepositoryService.LocationRepository.GetCollection();
                    RaisePropertyChanged("Locations");
                }
            }
        }

        public void Refresh(object obj)
        {
            Locations = RepositoryService.LocationRepository.GetCollection();
        }
    }
}
