using Assignment_2.ViewModels;
using System.Net.Sockets;
using System.Windows;

namespace Assignment_2
{
    public partial class Reports : Window
    {
        public Reports(TcpClient passedClient, NetworkStream passedStream)
        {
            InitializeComponent();

            // Set the ViewModel as the DataContext for the Reports window
            DataContext = new ReportsViewModel(passedClient, passedStream);
        }
    }
}
