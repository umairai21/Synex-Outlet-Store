using Assignment_2.Commands;
using Assignment_2.Services;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Windows.Input;
using System.Threading.Tasks;
using Assignment_2.Repository;

namespace Assignment_2.ViewModels
{
    public class CheckoutViewModel : BaseViewModel
    {
        private readonly CheckoutService _checkoutService;
        private readonly EventLoggerService _logger;

        public List<string> ItemCodes { get; set; }
        public string SelectedItemCode { get; set; }
        public string QuantityPurchased { get; set; }
        public string Discount { get; set; }
        public string WantsBill { get; set; }
        public string CashReceived { get; set; }

        public ICommand SubmitCommand { get; }
        public ICommand CalculateTotalCommand { get; }
        public ICommand BackCommand { get; }

        public async Task InitializeItemCodesAsync()
        {
            ItemCodes = await _checkoutService.GetItemCodesAsync();
        }

        public CheckoutViewModel(TcpClient client, NetworkStream stream)
        {
            var itemRepository = new ItemRepository();
            _checkoutService = new CheckoutService(itemRepository);
            _logger = EventLoggerService.GetInstance(client, stream); // Singleton logger instance

            // Initialize ItemCodes asynchronously
            Task.Run(async () => await InitializeItemCodesAsync());

            SubmitCommand = new RelayCommand(async () => await SubmitAsync());
            CalculateTotalCommand = new RelayCommand(async () => await CalculateTotalAsync());
            BackCommand = new RelayCommand(GoBack);
        }

        private async Task SubmitAsync()
        {
            if (string.IsNullOrWhiteSpace(SelectedItemCode) || string.IsNullOrWhiteSpace(QuantityPurchased) || string.IsNullOrWhiteSpace(Discount))
            {
                _logger.LogEvent("Missing or invalid inputs.");
                return;
            }

            bool wantsBill = WantsBill.ToLower() == "yes";
            decimal discount = decimal.Parse(Discount);
            decimal cashReceived = decimal.Parse(CashReceived);
            decimal totalAmount = await _checkoutService.CalculateTotalAmountAsync(SelectedItemCode, int.Parse(QuantityPurchased), discount);

            if (wantsBill)
            {
                _checkoutService.ShowBillPopUp(totalAmount, discount, cashReceived);
                _logger.LogEvent("Bill generated.");
            }

            await _checkoutService.ProcessPurchaseAsync(SelectedItemCode, int.Parse(QuantityPurchased));
            _logger.LogEvent($"Purchase processed for {SelectedItemCode}, quantity {QuantityPurchased}");
        }

        private async Task CalculateTotalAsync()
        {
            if (string.IsNullOrWhiteSpace(SelectedItemCode) || string.IsNullOrWhiteSpace(QuantityPurchased) || string.IsNullOrWhiteSpace(Discount))
            {
                _logger.LogEvent("Invalid inputs for calculating total.");
                return;
            }

            decimal discount = decimal.Parse(Discount);
            decimal totalAmount = await _checkoutService.CalculateTotalAmountAsync(SelectedItemCode, int.Parse(QuantityPurchased), discount);
            // Trigger any notification if needed.
        }

        private void GoBack()
        {
            // Implement the logic to navigate back to previous window
            _logger.LogEvent("Navigating back.");
        }
    }
}
