using System.Collections.Generic;
using System.Windows.Input;
using Assignment_2.Services;
using Assignment_2.Commands;
using Assignment_2.DTO;
using System.IO;
using System.Net.Sockets;

using Assignment_2.Repository;


namespace Assignment_2.ViewModels
{
    public class ReportsViewModel : BaseViewModel
    {
        private readonly TcpClient _client;   // Declare _client
        private readonly NetworkStream _stream; // Declare _stream
        private readonly ReportsRepository _reportsRepository;
        private readonly EventLoggerService _logger;

        public List<BillItemDTO> TotalSalesReport { get; set; }
        public List<Repository.StockWithItemDTO> ReshelveReport { get; set; }
        public List<Repository.StockWithItemDTO> ReorderReport { get; set; }
        public List<Repository.StockWithItemDTO> StockReport { get; set; }
        public List<BillDTO> BillReport { get; set; }

        // Commands for binding to buttons in XAML
        public ICommand ShowTotalSalesReportCommand { get; }
        public ICommand ShowReshelveReportCommand { get; }
        public ICommand ShowReorderReportCommand { get; }
        public ICommand ShowStockReportCommand { get; }
        public ICommand ShowBillReportCommand { get; }
        public ICommand BackCommand { get; }

        public ReportsViewModel(TcpClient client, NetworkStream stream)
        {
            _reportsRepository = new ReportsRepository();
            _logger = EventLoggerService.GetInstance(client, stream);  // Singleton pattern for logger
            ShowTotalSalesReportCommand = new RelayCommand(async () => await ShowTotalSalesReportAsync());
            ShowReshelveReportCommand = new RelayCommand(async () => await ShowReshelveReportAsync());
            ShowReorderReportCommand = new RelayCommand(async () => await ShowReorderReportAsync());
            ShowStockReportCommand = new RelayCommand(async () => await ShowStockReportAsync());
            ShowBillReportCommand = new RelayCommand(async () => await ShowBillReportAsync());
            BackCommand = new RelayCommand(GoBack);
        }
        // Methods to load data into the corresponding report lists
        private async Task ShowTotalSalesReportAsync()
        {
            TotalSalesReport = await _reportsRepository.GetTotalSalesReportAsync();
            OnPropertyChanged(nameof(TotalSalesReport));
            _logger.LogEvent("Total Sales Report loaded.");
        }

        private async Task ShowReshelveReportAsync()
        {
            ReshelveReport = await _reportsRepository.GetReshelveReportAsync();
            OnPropertyChanged(nameof(ReshelveReport));
            _logger.LogEvent("Reshelve Report loaded.");
        }

        private async Task ShowReorderReportAsync()
        {
            ReorderReport = await _reportsRepository.GetReorderReportAsync();
            OnPropertyChanged(nameof(ReorderReport));
            _logger.LogEvent("Reorder Report loaded.");
        }

        private async Task ShowStockReportAsync()
        {
            StockReport = await _reportsRepository.GetStockReportAsync();
            OnPropertyChanged(nameof(StockReport));
            _logger.LogEvent("Stock Report loaded.");
        }

        private async Task ShowBillReportAsync()
        {
            BillReport = await _reportsRepository.GetBillReportAsync();
            OnPropertyChanged(nameof(BillReport));
            _logger.LogEvent("Bill Report loaded.");
        }
        private void GoBack()
        {
            // Logic to navigate back to the homepage
            var homePage = new homePage(_client, _stream);
            homePage.Show();
            System.Windows.Application.Current.Windows[0]?.Close();
        }

    }
}
