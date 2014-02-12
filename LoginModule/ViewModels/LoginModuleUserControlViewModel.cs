using Interfaces.utilities.Command;
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
    class LoginModuleUserControlViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

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
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Login"));
                }
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
