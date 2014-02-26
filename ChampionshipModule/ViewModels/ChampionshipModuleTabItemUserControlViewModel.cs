using ChampionshipModule.Views;
using Interfaces.Events;
using Interfaces.PersisenceModule.Datamodule;
using Interfaces.PersisenceModule.Services;
using Interfaces.utilities.Command;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChampionshipModule.ViewModels
{
    public class ChampionshipModuleTabItemUserControlViewModel : NotificationObject
    {
        public ChampionshipModuleTabItemUserControlViewModel(IRepositoryService repositoryService, IEventAggregator eventAggregator, IUnityContainer unityContainer)
        {
            RepositoryService = repositoryService;
            EventAggregator = eventAggregator;
            UnityContainer = unityContainer;

            EventAggregator.GetEvent<RefreshViewsEvent>().Subscribe(Refresh);
        }

        private IEventAggregator EventAggregator { get; set; }
        private IRepositoryService RepositoryService { get; set; }
        private IUnityContainer UnityContainer { get; set; }

        private ObservableCollection<IChampionship> championships;
        public ObservableCollection<IChampionship> Championships 
        {
            get
            {
                if (championships == null)
                {
                    championships = RepositoryService.ChampionshipRepository.GetCollection();
                }
                return championships;
            }
            set
            {
                championships = value;
                RaisePropertyChanged("Championships");
                
            }
        }

        private ObservableCollection<INation> nations;
        public ObservableCollection<INation> Nations
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

        private ICommand addChampionshipOkCommand;
        public ICommand AddChampionshipOkCommand
        {
            get
            {
                if (addChampionshipOkCommand == null)
                {
                    addChampionshipOkCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var tabitem = o as ChampionshipModuleTabItemUserControl;

                            IChampionship newChampionship = UnityContainer.Resolve<IChampionship>();

                            INation selectedNation = tabitem.nationscombobox.SelectedItem as INation;

                            newChampionship.StartsAt = tabitem.startsatdatetimepicker.SelectedDate.HasValue ? tabitem.startsatdatetimepicker.SelectedDate.Value : DateTime.Now;
                            newChampionship.EndsAt = tabitem.endsatdatetimepicker.SelectedDate.HasValue ? tabitem.startsatdatetimepicker.SelectedDate.Value : DateTime.Now;
                            newChampionship.NationId = selectedNation.Id;
                            newChampionship.Name = selectedNation.Name + " " + newChampionship.StartsAt.Year.ToString();

                            RepositoryService.ChampionshipRepository.Post(newChampionship);

                            Refresh(new object());

                            tabitem.gridnewchampionship.Visibility = Visibility.Collapsed;
                            tabitem.datagridchampionships.Visibility = Visibility.Visible;
                            tabitem.addchampionshipbutton.Visibility = Visibility.Visible;
                        });
                }
                return addChampionshipOkCommand;
            }
        }

        private ICommand addChampionshipCancelCommand;
        public ICommand AddChampionshipCancelCommand
        {
            get
            {
                if (addChampionshipCancelCommand == null)
                {
                    addChampionshipCancelCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var tabitem = o as ChampionshipModuleTabItemUserControl;
                            tabitem.gridnewchampionship.Visibility = Visibility.Collapsed;
                            tabitem.datagridchampionships.Visibility = Visibility.Visible;
                            tabitem.addchampionshipbutton.Visibility = Visibility.Visible;

                        });
                }
                return addChampionshipCancelCommand;
            }
        }

        private ICommand addChampionshipCommand;
        public ICommand AddChampionshipCommand
        {
            get
            {
                if (addChampionshipCommand == null)
                {
                    addChampionshipCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var tabitem = o as ChampionshipModuleTabItemUserControl;
                            tabitem.gridnewchampionship.Visibility = Visibility.Visible;
                            tabitem.datagridchampionships.Visibility = Visibility.Collapsed;
                            tabitem.addchampionshipbutton.Visibility = Visibility.Collapsed;
                            
                            
                        });
                }
                return addChampionshipCommand;
            }
        }

        public void Refresh(object obj)
        {
            Championships = RepositoryService.ChampionshipRepository.GetCollection();
            Nations = RepositoryService.NationRepository.GetCollection();
        }
    }
}
