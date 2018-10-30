using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace kurs
{
    public class BaseCommand:ICommand
    {
        Action _executeMethod;
        Func<bool> _canExecuteMethod;
        public BaseCommand(Action p_executeMethod, Func<bool> p_canExecuteMethod = null)
        {
            _executeMethod = p_executeMethod;
            _canExecuteMethod = p_canExecuteMethod ?? (() => true);
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecuteMethod != null ? _canExecuteMethod() : false;
        }

        public void Execute(object parameter)
        {
            if (_executeMethod != null)
            { _executeMethod(); }
        }
    }
}

