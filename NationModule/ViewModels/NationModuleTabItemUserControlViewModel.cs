using Interfaces.Events;
using Interfaces.PersisenceModule.Datamodule;
using Interfaces.PersisenceModule.Services;
using Interfaces.utilities.Command;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using NationModule.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NationModule.ViewModels
{
    public class NationModuleTabItemUserControlViewModel : NotificationObject
    {
        public NationModuleTabItemUserControlViewModel(IRepositoryService repositoryService, IEventAggregator eventAggregator, IUnityContainer unityContainer)
        {
            RepositoryService = repositoryService;
            EventAggregator = eventAggregator;
            UnityContainer = unityContainer;

            EventAggregator.GetEvent<RefreshViewsEvent>().Subscribe(Refresh);
        }

        private IEventAggregator EventAggregator { get; set; }
        private IRepositoryService RepositoryService { get; set; }
        private IUnityContainer UnityContainer { get; set; }

        private BindingList<INation> nations;
        public BindingList<INation> Nations
        {
            get
            {
                if (nations == null)
                {
                    nations = RepositoryService.NationRepository.GetCollection();
                }
                return nations;
            }
            set
            {
                nations = value;
                RaisePropertyChanged("Nations");
            }
        }

        private ICommand addNationOkCommand;
        public ICommand AddNationOkCommand
        {
            get
            {
                if (addNationOkCommand == null)
                {
                    addNationOkCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var tabitem = o as NationModuleTabItemUserControl;

                            INation newNation = UnityContainer.Resolve<INation>();

                            newNation.Name = tabitem.nationnametextbox.Text;
                            newNation.Continent = tabitem.nationcontinenttextbox.Text;

                            RepositoryService.NationRepository.Post(newNation);

                            Refresh(new object());

                            tabitem.gridnewnation.Visibility = Visibility.Collapsed;
                            tabitem.datagridnations.Visibility = Visibility.Visible;
                            tabitem.addnationbutton.Visibility = Visibility.Visible;
                            tabitem.addnewnationcancel.Visibility = Visibility.Collapsed;
                        });
                }
                return addNationOkCommand;
            }
        }

        private ICommand addNationCancelCommand;
        public ICommand AddNationCancelCommand
        {
            get
            {
                if (addNationCancelCommand == null)
                {
                    addNationCancelCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var tabitem = o as NationModuleTabItemUserControl;
                            tabitem.gridnewnation.Visibility = Visibility.Collapsed;
                            tabitem.datagridnations.Visibility = Visibility.Visible;
                            tabitem.addnationbutton.Visibility = Visibility.Visible;
                            tabitem.addnewnationcancel.Visibility = Visibility.Collapsed;

                        });
                }
                return addNationCancelCommand;
            }
        }

        private ICommand addNationCommand;
        public ICommand AddNationCommand
        {
            get
            {
                if (addNationCommand == null)
                {
                    addNationCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var tabitem = o as NationModuleTabItemUserControl;
                            tabitem.gridnewnation.Visibility = Visibility.Visible;
                            tabitem.datagridnations.Visibility = Visibility.Collapsed;
                            tabitem.addnationbutton.Visibility = Visibility.Collapsed;
                            tabitem.addnewnationcancel.Visibility = Visibility.Visible;

                        });
                }
                return addNationCommand;
            }
        }

        public void Refresh(object obj)
        {
            Nations = RepositoryService.NationRepository.GetCollection();
        }
    }
}
