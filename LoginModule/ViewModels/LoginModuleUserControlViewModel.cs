using Interfaces.Events;
using Interfaces.PersisenceModule.Datamodule;
using Interfaces.PersisenceModule.Services;
using Interfaces.utilities.Command;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace LoginModule.ViewModels
{
    public class LoginModuleUserControlViewModel : NotificationObject
    {
        public LoginModuleUserControlViewModel(IRepositoryService repositoryService, IEventAggregator eventAggregator, IUnityContainer unityContainer)
        {
            RepositoryService = repositoryService;
            EventAggregator = eventAggregator;
            UnityContainer = unityContainer;
        }

        private IEventAggregator EventAggregator { get; set; }
        private IRepositoryService RepositoryService { get; set; }
        private IUnityContainer UnityContainer { get; set; }

        private string login = "ante";
        public string Login 
        {
            get
            {
                return login;
            }
            set
            {
                login = value;
                RaisePropertyChanged("Login");
            }
        }

        private ICommand connectCommand;
        public ICommand ConnectCommand
        {
            get
            {
                if (connectCommand == null)
                {
                    connectCommand = new MyCommand(
                        (obj) =>
                        {
                            return true;
                        },
                        (passwordBox) =>
                        {
                            var pwBox = passwordBox as PasswordBox;
                            var password = pwBox.Password;

                            EventAggregator.GetEvent<LoginAndPasswordChangedEvent>().Publish(new Tuple<string, string>(Login, password));
                            EventAggregator.GetEvent<RefreshViewsEvent>().Publish("");


                            // location
                            var locations = RepositoryService.LocationRepository.GetCollection();
                            var alocation = locations[0];

                            alocation.Name = "c# " + DateTime.Now.ToString("R");

                            locations[0] = alocation;
                            

                            // championship
                            var championships = RepositoryService.ChampionshipRepository.GetCollection();

                            var one = championships[0];
                            one.Name = " c# " + DateTime.Now.ToString("R");

                            championships[0] = one;

                            EventAggregator.GetEvent<UserLoggedInEvent>().Publish("");
                        });
                }

                return connectCommand;
            }
        }

    }
}
