using ChampionshipModule.Views;
using Interfaces.Events;
using Interfaces.PersisenceModule.Datamodule;
using Interfaces.PersisenceModule.Services;
using Interfaces.utilities.Command;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ChampionshipModule.ViewModels
{
    public class ChampionshipModuleTabItemUserControlViewModel : NotificationObject
    {
        public ChampionshipModuleTabItemUserControlViewModel(IRepositoryService repositoryService, IEventAggregator eventAggregator, IUnityContainer unityContainer, IRegionManager regionManager)
        {
            RepositoryService = repositoryService;
            EventAggregator = eventAggregator;
            UnityContainer = unityContainer;
            RegionManager = regionManager;
                
            EventAggregator.GetEvent<RefreshViewsEvent>().Subscribe(Refresh);
        }

        private IEventAggregator EventAggregator { get; set; }
        private IRepositoryService RepositoryService { get; set; }
        private IUnityContainer UnityContainer { get; set; }
        private IRegionManager RegionManager { get; set; }

        public IChampionship selectedChampionship;
        public IChampionship SelectedChampionship
        { 
            get 
            { 
                return selectedChampionship; 
            } 
            set 
            {
                selectedChampionship = value;
                RaisePropertyChanged("SelectedChampionship");

                Organizer = Nations.First(nation => nation.Id == SelectedChampionship.NationId);
                Groups = RepositoryService.GroupRepository(selectedChampionship).GetCollection();
                Teams = RepositoryService.TeamRepository(selectedChampionship).GetCollection();
                Matches = RepositoryService.MatchRepository(selectedChampionship).GetCollection();
            } 
        }

        public INation organizer;
        public INation Organizer
        {
            get
            {
                return organizer;
            }
            set
            {
                organizer = value;
                RaisePropertyChanged("Organizer");

            }
        }

        private BindingList<IGroup> groups;
        public BindingList<IGroup> Groups
        {
            get
            {
                return groups;
            }
            set
            {
                groups = value;
                RaisePropertyChanged("Groups");
            }
        }

        private BindingList<ITeam> teams;
        public BindingList<ITeam> Teams
        {
            get
            {
                return teams;
            }
            set
            {
                teams = value;
                RaisePropertyChanged("Teams");
            }
        }

        private BindingList<IMatch> matches;
        public BindingList<IMatch> Matches
        {
            get
            {
                return matches;
            }
            set
            {
                matches = value;
                RaisePropertyChanged("Matches");
            }
        }

        private BindingList<IChampionship> championships;
        public BindingList<IChampionship> Championships 
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

        private ICommand rowDoubleClickCommand;
        public ICommand RowDoubleClickCommand
        {
            get
            {
                if (rowDoubleClickCommand == null)
                {
                    rowDoubleClickCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var tuple = o as Tuple<IChampionship, ChampionshipModuleTabItemUserControl>;
                            SelectedChampionship = tuple.Item1;
                            Debug.WriteLine(tuple.Item1.Name);
                            
                            tuple.Item2.overviewgrid.Visibility = Visibility.Collapsed;
                            tuple.Item2.detailgrid.Visibility = Visibility.Visible;
                            
                        });
                }
                return rowDoubleClickCommand;
            }
        }

        private ICommand displayGroupsCommand;
        public ICommand DisplayGroupsCommand
        {
            get
            {
                if (displayGroupsCommand == null)
                {
                    displayGroupsCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var championshipDetailsRegion = RegionManager.Regions["ChampionshipDetailsRegion"];
                            var groupsUserControl = UnityContainer.Resolve<GroupsUserControl>();
                            foreach (var view in championshipDetailsRegion.Views)
                            {
                                championshipDetailsRegion.Remove(view);
                            }
                            championshipDetailsRegion.Add(groupsUserControl);
                        });
                }
                return displayGroupsCommand;
            }
        }

        private ICommand displayOverviewCommand;
        public ICommand DisplayOverviewCommand
        {
            get
            {
                if (displayOverviewCommand == null)
                {
                    displayOverviewCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var championshipDetailsRegion = RegionManager.Regions["ChampionshipDetailsRegion"];
                            var overviewUserControl = UnityContainer.Resolve<OverviewUserControl>();
                            foreach (var view in championshipDetailsRegion.Views)
                            {
                                championshipDetailsRegion.Remove(view);
                            }
                            championshipDetailsRegion.Add(overviewUserControl);
                        });
                }
                return displayOverviewCommand;
            }
        }

        private ICommand displayTeamsCommand;
        public ICommand DisplayTeamsCommand
        {
            get
            {
                if (displayTeamsCommand == null)
                {
                    displayTeamsCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var championshipDetailsRegion = RegionManager.Regions["ChampionshipDetailsRegion"];
                            var teamsUserControl = UnityContainer.Resolve<TeamsUserControl>();
                            foreach (var view in championshipDetailsRegion.Views)
                            {
                                championshipDetailsRegion.Remove(view);
                            }
                            championshipDetailsRegion.Add(teamsUserControl);
                        });
                }
                return displayTeamsCommand;
            }
        }

        private ICommand displayMatchesCommand;
        public ICommand DisplayMatchesCommand
        {
            get
            {
                if (displayMatchesCommand == null)
                {
                    displayMatchesCommand = new MyCommand(
                        (o) => true,
                        (o) =>
                        {
                            var championshipDetailsRegion = RegionManager.Regions["ChampionshipDetailsRegion"];
                            var matchesUserControl = UnityContainer.Resolve<MatchesUserControl>();
                            foreach (var view in championshipDetailsRegion.Views)
                            {
                                championshipDetailsRegion.Remove(view);
                            }
                            championshipDetailsRegion.Add(matchesUserControl);
                        });
                }
                return displayMatchesCommand;
            }
        }

        public void Refresh(object obj)
        {
            Championships = RepositoryService.ChampionshipRepository.GetCollection();
            Nations = RepositoryService.NationRepository.GetCollection();
        }
    }
}
