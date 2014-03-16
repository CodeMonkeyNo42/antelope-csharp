using Interfaces.Events;
using Interfaces.PersisenceModule.Datamodule;
using Interfaces.PersisenceModule.Services;
using Interfaces.utilities.Command;
using LocationModule.Views;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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

        private BindingList<ILocation> locations;
        public BindingList<ILocation> Locations 
        {
            get
            {
                if (locations == null)
                {
                    locations = RepositoryService.LocationRepository.GetCollection();
                }
                return locations;
            }
            set
            {
                locations = value;
                RaisePropertyChanged("Locations");
            }
        }

        private ICommand addLocationOkCommand;
        public ICommand AddLocationOkCommand
        {
            get
            {
                if (addLocationOkCommand == null)
                {
                    addLocationOkCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var tabitem = o as LocationModuleTabItemUserControl;

                            ILocation newLocation = UnityContainer.Resolve<ILocation>();
                            newLocation.Name = tabitem.locationtextbox.Text;

                            RepositoryService.LocationRepository.Post(newLocation);

                            Refresh(new object());

                            tabitem.gridnewlocation.Visibility = Visibility.Collapsed;
                            tabitem.listboxlocations.Visibility = Visibility.Visible;
                            tabitem.addlocationbutton.Visibility = Visibility.Visible;
                        });
                }
                return addLocationOkCommand;
            }
        }

        private ICommand addLocationCancelCommand;
        public ICommand AddLocationCancelCommand
        {
            get
            {
                if (addLocationCancelCommand == null)
                {
                    addLocationCancelCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var tabitem = o as LocationModuleTabItemUserControl;
                            tabitem.gridnewlocation.Visibility = Visibility.Collapsed;
                            tabitem.listboxlocations.Visibility = Visibility.Visible;
                            tabitem.addlocationbutton.Visibility = Visibility.Visible;
                        });
                }
                return addLocationCancelCommand;
            }
        }

        private ICommand addLocationCommand;
        public ICommand AddLocationCommand
        {
            get
            {
                if (addLocationCommand == null)
                {
                    addLocationCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var tabitem = o as LocationModuleTabItemUserControl;
                            tabitem.gridnewlocation.Visibility = Visibility.Visible;
                            tabitem.listboxlocations.Visibility = Visibility.Collapsed;
                            tabitem.addlocationbutton.Visibility = Visibility.Collapsed;
                        });
                }
                return addLocationCommand;
            }
        }

        public void Refresh(object obj)
        {
            Locations = RepositoryService.LocationRepository.GetCollection();
        }
    }
}
