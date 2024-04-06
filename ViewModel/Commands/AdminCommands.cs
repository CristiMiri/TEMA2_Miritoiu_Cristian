using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PS_TEMA2.ViewModel.Commands
{
    class AdminCommands : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action action;

        public AdminCommands(Action action)
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
