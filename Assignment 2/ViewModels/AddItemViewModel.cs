using Assignment_2.Commands;
using Assignment_2.Services;
using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using Assignment_2.Repository;

namespace Assignment_2.ViewModels
{
    public class AddItemViewModel : BaseViewModel
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly EventLoggerService _logger;
        private readonly ItemRepository _itemRepository;

        public ICommand SubmitCommand { get; }
        public ICommand GoToInventoryCommand { get; }
        public ICommand BackCommand { get; }

        public string ItemCode { get; set; }
        public string ItemName { get; set; }
        public string ItemPrice { get; set; }

        // Constructor: Accepts TcpClient, NetworkStream and initializes dependencies
        public AddItemViewModel(TcpClient client, NetworkStream stream)
        {
            _client = client;
            _stream = stream;
            _logger = EventLoggerService.GetInstance(_client, _stream); // Singleton pattern for logger
            _itemRepository = new ItemRepository(); // Initialize the ItemRepository

            SubmitCommand = new RelayCommand(async () => await SubmitAsync()); // Command for submitting the form
            GoToInventoryCommand = new RelayCommand(GoToInventory); // Command for navigating to inventory
            BackCommand = new RelayCommand(GoBack); // Command for navigating back
        }

        // Method to handle the item submission process
        public async Task SubmitAsync()
        {
            try
            {
                // Validate price input
                if (!decimal.TryParse(ItemPrice, out decimal price))
                {
                    System.Windows.MessageBox.Show("Please enter a valid price.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _logger.LogEvent("Invalid price entry.");
                    return;
                }

                // Validate required fields
                if (string.IsNullOrWhiteSpace(ItemCode) || string.IsNullOrWhiteSpace(ItemName))
                {
                    System.Windows.MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _logger.LogEvent("Missing fields in the add item form.");
                    return;
                }

                // Save the item using ItemRepository
                _itemRepository.AddItemAsync(ItemCode, ItemName, price);

                // Display success message
                System.Windows.MessageBox.Show("Item added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _logger.LogEvent($"Item {ItemName} added successfully.");

                // Clear fields after submission
                ItemCode = string.Empty;
                ItemName = string.Empty;
                ItemPrice = string.Empty;

                OnPropertyChanged(nameof(ItemCode)); // Update UI with cleared values
                OnPropertyChanged(nameof(ItemName));
                OnPropertyChanged(nameof(ItemPrice));

                // Ask if the user wants to proceed to the inventory page
                var result = System.Windows.MessageBox.Show(
                    "You should now add this item into the stock. Do you want to proceed to the inventory page?",
                    "Add Item to Stock",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Information);

                if (result == MessageBoxResult.Yes)
                {
                    // Redirect to inventory page
                    GoToInventory();
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the save operation
                System.Windows.MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.LogEvent($"Error adding item: {ex.Message}");
            }
        }

        // Method to navigate to inventory page
        public void GoToInventory()
        {
            try
            {
                var inventoryPage = new manageInventory(_client, _stream); // Auto-fill the ItemCode
                inventoryPage.Show();
                System.Windows.Application.Current.Windows[0].Close(); // Close the current window
                _logger.LogEvent("Navigated to Inventory page.");
            }
            catch (Exception ex)
            {
                _logger.LogEvent($"Error navigating to Inventory page: {ex.Message}");
            }
        }

        // Method to navigate back to the home page
        public void GoBack()
        {
            try
            {
                var homePage = new homePage(_client, _stream);
                homePage.Show();
                System.Windows.Application.Current.Windows[0].Close(); // Close the current window
                _logger.LogEvent("Navigated back to Home page.");
            }
            catch (Exception ex)
            {
                _logger.LogEvent($"Error navigating back to Home page: {ex.Message}");
            }
        }
    }
}
