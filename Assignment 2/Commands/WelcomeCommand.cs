using System;
using System.Net.Sockets;
using System.Windows.Input;

namespace Assignment_2.Commands
{
    public class WelcomeCommand : ICommand
    {
        private TcpClient _client;
        private NetworkStream _stream;
        public event EventHandler CanExecuteChanged;

        public WelcomeCommand(TcpClient client, NetworkStream stream)
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
            homePage homePage = new homePage(_client, _stream);
            homePage.Show();
        }
    }
}
