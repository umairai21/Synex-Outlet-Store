using Assignment_2.ViewModels;
using System.Net.Sockets;
using System.Windows;

namespace Assignment_2
{
    public partial class displayItems : Window
    {
        private readonly DisplayItemsViewModel _viewModel;

        public displayItems(TcpClient client, NetworkStream stream)
        {
            InitializeComponent();
            _viewModel = new DisplayItemsViewModel(client, stream);  // Initialize the ViewModel
            DataContext = _viewModel;  // Set the DataContext for data binding
        }

        // Ensure that the timer is stopped when the window is closed
        protected override void OnClosed(System.EventArgs e)
        {
            _viewModel.StopRefreshTimer();  // Stop and dispose of the timer in ViewModel
            base.OnClosed(e);
        }


        // Define the Window_Loaded event handler
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.LoadItemsAsync(); // Call LoadItemsAsync method to populate data when the window is loaded
        }

        // Event handler for the Back button (can be bound in XAML if preferred)
        private void btn1_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.GoBack();  // Delegate the logic to ViewModel
        }
    }
}
