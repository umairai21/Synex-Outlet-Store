using System;
using System.Net.Sockets;
using System.Windows;
using System.Windows.Input;

namespace Assignment_2.Commands
{
    public class ExitCommand : ICommand
    {
        private TcpClient _client;
        private NetworkStream _stream;
        public event EventHandler CanExecuteChanged;

        public ExitCommand(TcpClient client, NetworkStream stream)
        {
            _client = client;
            _stream = stream;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _stream?.Close();
            _client?.Close();
            System.Windows.Application.Current.Shutdown();
        }
    }
}
