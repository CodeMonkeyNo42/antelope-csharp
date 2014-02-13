using Interfaces.utilities.Command;
using Microsoft.Practices.Prism.ViewModel;
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
    class LoginModuleUserControlViewModel : NotificationObject
    {

        private string login = "initial login";
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
                            MessageBox.Show(password);
                        });
                }

                return connectCommand;
            }
        }

    }
}
