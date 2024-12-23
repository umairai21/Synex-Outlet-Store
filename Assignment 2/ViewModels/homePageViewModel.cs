using Assignment_2.Commands;
using Assignment_2.Services;
using System.Net.Sockets;
using System.Windows.Navigation;

namespace Assignment_2.ViewModels
{
    public class homePageViewModel
    {
        private TcpClient _client;
        private NetworkStream _stream;
        private readonly EventLoggerService _logger;

        public NavigateCommand AddItemCommand { get; }
        public NavigateCommand ManageStockCommand { get; }
        public NavigateCommand DisplayItemsCommand { get; }
        public NavigateCommand ReportsCommand { get; }
        public NavigateCommand CheckoutCommand { get; }
        public NavigateCommand BackCommand { get; }

        public homePageViewModel(TcpClient client, NetworkStream stream, EventLoggerService logger)
        {
            _client = client;
            _stream = stream;
            _logger = logger;

            // Initialize commands with appropriate actions
            AddItemCommand = new NavigateCommand(NavigateToAddItem);
            ManageStockCommand = new NavigateCommand(NavigateToManageStock);
            DisplayItemsCommand = new NavigateCommand(NavigateToDisplayItems);
            ReportsCommand = new NavigateCommand(NavigateToReports);
            CheckoutCommand = new NavigateCommand(NavigateToCheckout);
            BackCommand = new NavigateCommand(NavigateBack);

            _logger.LogEvent("Navigated to Home Page");
        }

        private void NavigateToAddItem()
        {
            _logger.LogEvent("Navigated to Add Item Page.");
            Assignment_2.Services.NavigationService.NavigateTo(new addItemPage(_client, _stream));
        }

        private void NavigateToManageStock()
        {
            _logger.LogEvent("Navigated to Manage Stock Page.");
            Assignment_2.Services.NavigationService.NavigateTo(new manageInventory(_client, _stream));
        }

        private void NavigateToDisplayItems()
        {
            _logger.LogEvent("Navigated to Display Items Page.");
            Assignment_2.Services.NavigationService.NavigateTo(new displayItems(_client, _stream));
        }

        private void NavigateToReports()
        {
            _logger.LogEvent("Navigated to Reports Page.");
            Assignment_2.Services.NavigationService.NavigateTo(new Reports(_client, _stream));
        }

        private void NavigateToCheckout()
        {
            _logger.LogEvent("Navigated to Checkout Page.");
            Assignment_2.Services.NavigationService.NavigateTo(new Checkout(_client, _stream));
        }

        private void NavigateBack()
        {
            _logger.LogEvent("Navigated back to Main Window.");
            Assignment_2.Services.NavigationService.NavigateTo(new MainWindow());
        }
    }
}
