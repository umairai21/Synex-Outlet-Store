using Assignment_2.Services;
using Assignment_2.ViewModels;
using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Assignment_2
{
    public partial class homePage : Window
    {
        private readonly homePageViewModel _viewModel;

        public homePage(TcpClient client, NetworkStream stream)
        {
            InitializeComponent();
            _viewModel = new homePageViewModel(client, stream, EventLoggerService.GetInstance(client, stream));
            DataContext = _viewModel; // Bind the ViewModel to the view
        }
    }
}
