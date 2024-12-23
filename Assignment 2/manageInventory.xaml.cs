using Assignment_2.ViewModels;
using System.Net.Sockets;
using System.Windows;

namespace Assignment_2
{
    public partial class manageInventory : Window
    {
        private readonly ManageInventoryViewModel _viewModel;

        // Constructor accepting only TcpClient and NetworkStream to maintain connection
        public manageInventory(TcpClient client, NetworkStream stream)
        {
            InitializeComponent();
            _viewModel = new ManageInventoryViewModel(client, stream); // Create ViewModel
            DataContext = _viewModel; // Bind ViewModel to View
        }
    }
}
