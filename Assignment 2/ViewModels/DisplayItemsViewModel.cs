using Assignment_2.Commands;
using Assignment_2.Services;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using static Assignment_2.Reports;
using Assignment_2.Repository;


namespace Assignment_2.ViewModels
{
    public class DisplayItemsViewModel : BaseViewModel
    {
        private readonly TcpClient _client;
        private readonly NetworkStream _stream;
        private readonly ItemRepository _itemRepository;
        private readonly EventLoggerService _logger;
        private System.Timers.Timer _refreshTimer;

        public List<Assignment_2.Repository.StockWithItemDTO> Items { get; set; }   // Items to display in DataGrid

        public ICommand BackCommand { get; }

        public DisplayItemsViewModel(TcpClient client, NetworkStream stream)
        {
            _client = client;
            _stream = stream;
            _itemRepository = new ItemRepository();
            _logger = EventLoggerService.GetInstance(_client, _stream);  // Singleton logger

            BackCommand = new RelayCommand(GoBack);

            InitializeTimer();  // Set up timer for live data refresh

            LoadItemsAsync();  // Load items initially
        }

        // Initialize the timer for refreshing the stock data
        private void InitializeTimer()
        {
            _refreshTimer = new System.Timers.Timer(5000);  // 5 seconds interval
            _refreshTimer.Elapsed += RefreshTimerElapsed;
            _refreshTimer.AutoReset = true;
            _refreshTimer.Enabled = true;
        }

        // Timer event handler to refresh data
        private async void RefreshTimerElapsed(object sender, ElapsedEventArgs e)
        {
            await System.Windows.Application.Current.Dispatcher.Invoke(async () => await LoadItemsAsync());  // Ensure UI is updated on main thread
        }

        // Load the stock data into Items collection
        public async Task LoadItemsAsync()
        {
            try
            {
                Items = await _itemRepository.GetAllStocksWithItemInfoAsync();   // Get items from repository
                OnPropertyChanged(nameof(Items));  // Notify UI of changes
                _logger.LogEvent("Stock data loaded successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogEvent($"Error loading stock data: {ex.Message}");
            }
        }

        // Stop the timer when the window is closed
        public void StopRefreshTimer()
        {
            _refreshTimer?.Stop();
            _refreshTimer?.Dispose();
        }

        // Go back to home page logic
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
