using System;
using System.Windows;
using System.Net.Sockets;
using System.Text;
using Assignment_2.Services;
using Assignment_2.DTO;
using Assignment_2.Repository;

namespace Assignment_2
{
    public partial class Checkout : Window
    {
        private TcpClient client;
        private NetworkStream stream;
        private CheckoutService _checkoutService;

        // Constructor with dependency injection for TCP connections
        public Checkout(TcpClient passedClient, NetworkStream passedStream)
        {
            InitializeComponent();
            client = passedClient;
            stream = passedStream;
            var itemRepository = new ItemRepository();
            _checkoutService = new CheckoutService(itemRepository);  // Use CheckoutService for all backend logic
            PopulateItemCodesAsync();  // Populate ComboBox with item codes
        }

        // Method to populate item codes into the ComboBox
        private async Task PopulateItemCodesAsync()  // <-- Updated to async
        {
            cmbItemCode.ItemsSource = await _checkoutService.GetItemCodesAsync();  // <-- Async call
        }


        // Event handler for Calculate Total button
        private async void btnCalculateTotal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string itemCode = cmbItemCode.SelectedItem?.ToString();
                int quantity = int.Parse(txtQuantity.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);

                // Calculate the total using the CheckoutService
                decimal totalAmount = await _checkoutService.CalculateTotalAmountAsync(itemCode, quantity, discount);
                lblTotalPrice.Content = $"Total Price: {totalAmount:C}";
                lblTotalPrice.Visibility = Visibility.Visible;
                LogEvent($"Total calculated for item {itemCode}: {totalAmount:C}");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show($"Error calculating total: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                LogEvent($"Error calculating total: {ex.Message}");
            }
        }

        // Event handler for Submit button
        private async void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Retrieve input data
                string itemCode = cmbItemCode.SelectedItem?.ToString();
                int quantity = int.Parse(txtQuantity.Text);
                decimal discount = decimal.Parse(txtDiscount.Text);
                decimal cashReceived = decimal.Parse(txtCashReceived.Text);
                bool wantsBill = txtBill.Text.Trim().ToLower() == "yes";

                // Calculate total price after discount using the CheckoutService
                decimal totalAfterDiscount = await _checkoutService.CalculateTotalAmountAsync(itemCode, quantity, discount);

                // Process the purchase: update the stock
                await _checkoutService.ProcessPurchaseAsync(itemCode, quantity);

                // If customer wants a bill, generate and save the bill in the database
                if (wantsBill)
                {
                    // Generate a serial number for the bill (you can modify this logic as per your needs)
                    string serialNo = GenerateBillSerialNumber();
                    DateTime billDate = DateTime.Now;
                    decimal balance = cashReceived - totalAfterDiscount;

                    // Create a BillItemDTO list with all the items purchased (for now only one item is shown, modify as needed for multiple items)
                    var billItems = new List<BillItemDTO>
            {
                new Assignment_2.DTO.BillItemDTO
                {
                    ItemCode = itemCode,
                    ItemName = "Item Name",  // Modify this to fetch actual item name from DB if needed
                    Quantity = quantity,
                    Price = totalAfterDiscount / quantity,  // Price per item
                    TotalPrice = totalAfterDiscount  // Total price after discount
                }
            };

                    // Save the bill and bill items in the database using the CheckoutService
                    await _checkoutService.SaveBillAndBillItemsAsync(serialNo, billDate, totalAfterDiscount, discount, cashReceived, balance, billItems);

                    // Show the bill details to the user
                    _checkoutService.ShowBillPopUp(totalAfterDiscount, discount, cashReceived);
                }

                // Notify the user that the purchase was successful
                System.Windows.MessageBox.Show("Purchase processed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                LogEvent($"Purchase processed for item {itemCode}, quantity: {quantity}");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during processing
                System.Windows.MessageBox.Show($"Error processing purchase: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                LogEvent($"Error processing purchase: {ex.Message}");
            }
        }

        // Method to generate a unique serial number for the bill
        private string GenerateBillSerialNumber()
        {
            // Generate a random serial number for the bill (you can modify this logic)
            return Guid.NewGuid().ToString().Substring(0, 4); // Returns first 4 characters of GUID
        }


        // Simple logging method to capture events
        private void LogEvent(string message)
        {
            try
            {
                if (stream != null && client.Connected)
                {
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    stream.Flush();  // Immediately send the data
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging event: {ex.Message}");
            }
        }

        // Event handler for Back button
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close(); // Close the checkout window and return to the previous page
        }
    }
}
