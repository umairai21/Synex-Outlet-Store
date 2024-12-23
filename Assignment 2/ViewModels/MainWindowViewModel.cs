using Assignment_2.Commands;
using Assignment_2.Services;
using System.Net.Sockets;

namespace Assignment_2.ViewModels
{
    public class MainWindowViewModel
    {
        private TcpClient _client;
        private NetworkStream _stream;
        public WelcomeCommand WelcomeCmd { get; }
        public ExitCommand ExitCmd { get; }
        private EventLoggerService _logger; // Make logger a private field

        public MainWindowViewModel()
        {
            // Initialize logger before any network connection
            _logger = new EventLoggerService(_client, _stream);
            ConnectToServer();

            // Initialize commands
            WelcomeCmd = new WelcomeCommand(_client, _stream);
            ExitCmd = new ExitCommand(_client, _stream);

            _logger.LogEvent("Opened Home Page");
        }

        private void ConnectToServer()
        {
            try
            {
                _client = new TcpClient("127.0.0.1", 5000);
                _stream = _client.GetStream();

                // Update _logger with the correct client and stream
                _logger = new EventLoggerService(_client, _stream);
                _logger.LogEvent("Client connected.");
            }
            catch (Exception ex)
            {
                _logger.LogEvent($"Could not connect to server: {ex.Message}");
            }
        }
    }
}
