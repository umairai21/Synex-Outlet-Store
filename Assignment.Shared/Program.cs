using Assignment.Commands;
using Assignment.DTO;
using Assignment.Facade;
using Assignment.Factories;
using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        BillingSystemFacade facade = new BillingSystemFacade();
        ConcreteDTOFactory factory = new ConcreteDTOFactory();
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Add Item");
            Console.WriteLine("2. Manage Stock");
            Console.WriteLine("3. Display Items");
            Console.WriteLine("4. Generate Reports");
            Console.WriteLine("5. Checkout");
            Console.WriteLine("6. Exit");
            int option = int.Parse(Console.ReadLine());

            switch (option)
            {
                case 1:
                    var newItem = factory.CreateItemDTO();
                    Console.Write("Enter Item Code: ");
                    newItem.Code = Console.ReadLine();
                    Console.Write("Enter Item Name: ");
                    newItem.Name = Console.ReadLine();
                    Console.Write("Enter Item Price: ");
                    newItem.Price = decimal.Parse(Console.ReadLine());

                    // Create and execute the AddItemCommand
                    ICommand addItemCommand = new AddItemCommand(newItem, facade);
                    addItemCommand.Execute();

                    Console.WriteLine("Make sure to add this specific item to the inventory by the Manage Stock option");
                    break;

                case 2:
                    Console.WriteLine("Manage Stock");
                    var stock = factory.CreateStockDTO();
                    Console.Write("Enter Stock Code: ");
                    stock.StockCode = Console.ReadLine();
                    Console.Write("Enter Item Code: ");
                    var itemCode = Console.ReadLine();
                    var existingItem = facade.GetItemByCode(itemCode);
                    if (existingItem == null)
                    {
                        Console.WriteLine("Item not found.");
                        break;
                    }

                    Console.Write("Enter Quantity: ");
                    int quantity = int.Parse(Console.ReadLine());
                    Console.Write("Enter Expiry Date (yyyy-mm-dd): ");
                    stock.ExpiryDate = DateTime.Parse(Console.ReadLine());

                    var shelves = facade.GetShelves();
                    if (shelves.Count > 0)
                    {
                        Console.WriteLine("Select a Shelf:");
                        for (int i = 0; i < shelves.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {shelves[i].ShelfNo}");
                        }
                        int shelfOption = int.Parse(Console.ReadLine());
                        if (shelfOption < 1 || shelfOption > shelves.Count)
                        {
                            Console.WriteLine("Invalid shelf selection. Please try again.");
                            break;
                        }
                        string shelfNo = shelves[shelfOption - 1].ShelfNo;

                        // Create and execute the AddStockCommand
                        ICommand addStockCommand = new AddStockCommand(stock, itemCode, quantity, shelfNo, facade);
                        addStockCommand.Execute();

                        Console.WriteLine("Stock added successfully.");
                    }
                    else
                    {
                        Console.WriteLine("No shelves available. Please add shelves first.");
                    }
                    break;

                case 3:
                    // Display the inventory
                    facade.DisplayInventory();
                    break;

                case 4:
                    Console.WriteLine("Select a report to generate:");
                    Console.WriteLine("1. Total Sales Report");
                    Console.WriteLine("2. Reshelve Report");
                    Console.WriteLine("3. Reorder Report");
                    Console.WriteLine("4. Stock Report");
                    Console.WriteLine("5. Bill Report");
                    int reportOption = int.Parse(Console.ReadLine());
                    switch (reportOption)
                    {
                        case 1:
                            facade.GenerateSalesReport();
                            break;
                        case 2:
                            facade.GenerateReshelveReport();
                            break;
                        case 3:
                            facade.GenerateReorderReport();
                            break;
                        case 4:
                            facade.GenerateStockReport();
                            break;
                        case 5:
                            facade.GenerateBillReport();
                            break;
                        default:
                            Console.WriteLine("Invalid report option. Please try again.");
                            break;
                    }
                    break;

                case 5:
                    var purchasedItems = new List<ItemDTO>();
                    while (true)
                    {
                        Console.Write("Enter Item Code (or type 'done' to finish): ");
                        string purchasedCode = Console.ReadLine();
                        if (purchasedCode.ToLower() == "done") break;

                        Console.Write("Enter Quantity: ");
                        int purchasedQuantity = int.Parse(Console.ReadLine());

                        var purchasedItem = facade.GetItemByCode(purchasedCode);
                        if (purchasedItem == null)
                        {
                            Console.WriteLine("Item not found.");
                            continue;
                        }

                        purchasedItem.Quantity = purchasedQuantity;
                        purchasedItems.Add(purchasedItem);
                    }

                    Console.Write("Enter Discount: ");
                    float discount = float.Parse(Console.ReadLine());

                    decimal totalAmount = 0;
                    foreach (var purchasedItem in purchasedItems)
                    {
                        totalAmount += purchasedItem.Price * purchasedItem.Quantity;
                    }
                    decimal totalAfterDiscount = totalAmount - (decimal)discount;
                    Console.WriteLine($"Total Amount (after discount): {totalAfterDiscount}");

                    Console.Write("Do you want to generate the bill? (yes/no): ");
                    string generateBillResponse = Console.ReadLine();
                    if (generateBillResponse.ToLower() == "yes")
                    {
                        Console.Write("Enter Cash Received: ");
                        float cashReceived = float.Parse(Console.ReadLine());

                        // Perform checkout and generate the bill
                        facade.Checkout(purchasedItems, discount, cashReceived, out BillDTO bill);
                        facade.GenerateBill(bill);
                    }
                    break;

                case 6:
                    // Exit the application
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid option. Please try again.");
                    break;
            }
        }
    }
}
