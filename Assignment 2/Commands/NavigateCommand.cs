using System;
using System.Windows.Input;

namespace Assignment_2.Commands
{
    public class NavigateCommand : ICommand
    {
        private readonly Action _execute;

        public NavigateCommand(Action execute)
        {
            _execute = execute;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}
