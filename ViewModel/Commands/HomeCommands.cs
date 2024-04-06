using System;
using System.Windows.Input;

namespace PS_TEMA2.ViewModel.Commands
{
    public class HomeCommands : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action action;

        public HomeCommands(Action action)
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
