using Assignment_2.ViewModels;
using System;
using System.Net.Sockets;
using System.Text;
using System.Windows;

namespace Assignment_2
{
    public partial class MainWindow : Window
    {
        public MainWindowViewModel ViewModel { get; }

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainWindowViewModel();
            DataContext = ViewModel;  // Bind the ViewModel to the UI
        }
    }
}
