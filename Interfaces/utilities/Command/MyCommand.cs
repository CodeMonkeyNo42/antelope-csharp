using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Interfaces.utilities.Command
{
    public class MyCommand : ICommand
    {
        private Predicate<object> _canExecute;
        public Predicate<object> canExecute
        {
            get
            {
                return _canExecute;
            }
            set
            {
                _canExecute = value;
                if (CanExecuteChanged != null)
                {
                    CanExecuteChanged(this, new EventArgs());
                }
            }
        }

        private Action<object> execute;

        public MyCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this._canExecute = canExecute;
            this.execute = execute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            execute(parameter);
        }
    }
}
