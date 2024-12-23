using System.Windows;

namespace Assignment_2.Services
{
    public static class NavigationService
    {
        public static void NavigateTo(Window page)
        {
            page.Show();
            System.Windows.Application.Current.MainWindow?.Close(); // Close current window
            System.Windows.Application.Current.MainWindow = page;   // Set the new window as the current window
        }
    }
}
