using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PS_TEMA2.ViewModel.Commands
{
    internal class MainCommands:ICommand
    {
        private Action<string> _executeMethod;
        private Func<bool> _canExecuteMethod;

        public MainCommands(Action<string> executeMethod, Func<bool> canExecuteMethod = null)
        {
            _executeMethod = executeMethod;
            _canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return _canExecuteMethod == null || _canExecuteMethod();
        }

        public void Execute(object parameter)
        {
            if (parameter is string viewName)
            {
                _executeMethod(viewName);
            }
        }

        // Call this method when the can execute state changes
        public void OnCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
