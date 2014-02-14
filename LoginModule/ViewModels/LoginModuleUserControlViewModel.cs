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
        IUnityContainer UnityContainer { get; set; }

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

                            // debug
                            //MessageBox.Show(password);

                            // get
                            //var location = RepositoryService.LocationRepository.GetLocation(2);
                            //var locations = RepositoryService.LocationRepository.GetLocations();
                            //MessageBox.Show(locations.Count.ToString());
                            //MessageBox.Show(location.Name);
                            
                            // post
                            var newLoc = UnityContainer.Resolve<ILocation>();
                            newLoc.Name = "Test von c# " + DateTime.Now.ToString("R");
                            //var insertedLoc = RepositoryService.LocationRepository.PostLocation(newLoc);
                            //MessageBox.Show(insertedLoc.Id.ToString());

                            // put
                            var locations = RepositoryService.LocationRepository.GetLocations2();
                            //var location5 = locations.FirstOrDefault((i) => i.Id == 5);
                            //location5.Name += " changed";

                            locations.Add(newLoc);
                            MessageBox.Show(newLoc.Name);

                            // get
                            //var locations2 = RepositoryService.LocationRepository.GetLocations();
                            //MessageBox.Show(locations2.Count.ToString());
                        });
                }

                return connectCommand;
            }
        }

    }
}
