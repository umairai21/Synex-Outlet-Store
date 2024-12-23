using Assignment_2.Commands;
using Assignment_2.Services;
using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;
using Assignment_2.Repository;


namespace Assignment_2.ViewModels
{
    public class ManageInventoryViewModel : BaseViewModel
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly ItemRepository _itemRepository;
        private readonly EventLoggerService _logger;

        public List<string> ItemCodes { get; set; }
        public string SelectedItemCode { get; set; }

        public ICommand SubmitCommand { get; }
        public ICommand BackCommand { get; }

        public string StockCode { get; set; }
        public string ReceivedQuantity { get; set; }
        public string ExpiryDate { get; set; }
        public string ShelfNo { get; set; }

      
        public ManageInventoryViewModel(TcpClient client, NetworkStream stream)
        {
            _client = client;
            _stream = stream;
            _itemRepository = new ItemRepository();
            _logger = EventLoggerService.GetInstance(_client, _stream);

            // Make sure this line is correct
            LoadItemCodesAsync();

            

            SubmitCommand = new RelayCommand(async () => await SubmitAsync());
            BackCommand = new RelayCommand(GoBack);
        }

        // Asynchronous method to load item codes
        private async Task LoadItemCodesAsync()
        {
            try
            {
                ItemCodes = await _itemRepository.GetItemCodesAsync();
                OnPropertyChanged(nameof(ItemCodes)); // Notify the UI
            }
            catch (Exception ex)
            {
                _logger.LogEvent($"Error loading item codes: {ex.Message}");
            }
        }

        public async Task SubmitAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(StockCode) || string.IsNullOrWhiteSpace(SelectedItemCode) || string.IsNullOrWhiteSpace(ShelfNo))
                {
                    System.Windows.MessageBox.Show("Please fill in all fields.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _logger.LogEvent("Missing fields in the inventory form.");
                    return;
                }

                if (!int.TryParse(ReceivedQuantity, out int quantity))
                {
                    System.Windows.MessageBox.Show("Please enter a valid quantity.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _logger.LogEvent("Invalid quantity.");
                    return;
                }

                if (!DateTime.TryParse(ExpiryDate, out DateTime expiryDate))
                {
                    System.Windows.MessageBox.Show("Please enter a valid expiry date.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    _logger.LogEvent("Invalid expiry date.");
                    return;
                }

                // Call to the repository to save or update stock details
                await _itemRepository.SaveOrUpdateStockAsync(StockCode, SelectedItemCode, quantity, expiryDate, ShelfNo);

                System.Windows.MessageBox.Show("Inventory updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                _logger.LogEvent($"Inventory updated for stock code {StockCode}.");

                // Clear fields
                StockCode = string.Empty;
                ReceivedQuantity = string.Empty;
                ExpiryDate = string.Empty;
                ShelfNo = string.Empty;
                SelectedItemCode = null; // Clear the ComboBox selection
                OnPropertyChanged(nameof(StockCode));
                OnPropertyChanged(nameof(ReceivedQuantity));
                OnPropertyChanged(nameof(ExpiryDate));
                OnPropertyChanged(nameof(ShelfNo));
                OnPropertyChanged(nameof(SelectedItemCode)); // Notify that the ComboBox is cleared
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                _logger.LogEvent($"Error updating inventory: {ex.Message}");
            }
        }


        // Logic for navigating back
        public void GoBack()
        {
            try
            {
                var homePage = new homePage(_client, _stream);
                homePage.Show();
                System.Windows.Application.Current.Windows[0].Close();
                _logger.LogEvent("Navigated back to home page.");
            }
            catch (Exception ex)
            {
                _logger.LogEvent($"Error navigating back: {ex.Message}");
            }
        }
    }
}
