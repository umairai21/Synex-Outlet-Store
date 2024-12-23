using System;
using System.Windows.Input;
using Assignment_2.ViewModels;

namespace Assignment_2.Commands
{
    public class SubmitCommand : ICommand
    {
        private readonly AddItemViewModel _viewModel;

        public SubmitCommand(AddItemViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _viewModel.SubmitAsync(); // Call the Submit method in the ViewModel
        }
    }
}
