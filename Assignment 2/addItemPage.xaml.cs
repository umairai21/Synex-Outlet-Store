using System;
using System.Data.SqlClient;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Assignment_2.ViewModels;
using Microsoft.Data.SqlClient;

namespace Assignment_2
{
       public partial class addItemPage : Window
        {
        private readonly string _connectionString = "Data Source=UMAIR-CHANGE8\\SQLEXPRESS;Initial Catalog=SynexOutletStore;Integrated Security=True;Encrypt=False;";

        private readonly AddItemViewModel _viewModel;

            public addItemPage(TcpClient passedClient, NetworkStream passedStream)
            {
                InitializeComponent();
                _viewModel = new AddItemViewModel(passedClient, passedStream);
                DataContext = _viewModel; // Bind the ViewModel to the view
            }

        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            await _viewModel.SubmitAsync();
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
            {
                _viewModel.GoBack();
            }

            private void btnGoToInventory_Click(object sender, RoutedEventArgs e)
            {
                _viewModel.GoToInventory();
            }
        }
}
