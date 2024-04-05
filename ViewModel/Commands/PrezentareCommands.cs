using System;
using System.Windows.Input;

namespace PS_TEMA2.ViewModel.Commands
{
    public class PrezentareCommands : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action action;

        public PrezentareCommands(Action action)
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
