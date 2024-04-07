using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PS_TEMA2.ViewModel.Commands
{
    internal class LoginCommands : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action action;

        public LoginCommands(Action action)
        {
            this.action = action;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            this.action();
        }
    }
}
