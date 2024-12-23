using System.Net.Sockets;
using System.Text;

namespace Assignment_2.Services
{
    public class EventLoggerService
    {
        
        private static EventLoggerService _instance;
        private TcpClient _client;
        private NetworkStream _stream;

        // Change this constructor to public
        public EventLoggerService(TcpClient client, NetworkStream stream)
        {
            _client = client;
            _stream = stream;
        }

        // Singleton GetInstance method
        public static EventLoggerService GetInstance(TcpClient client, NetworkStream stream)
        {
            if (_instance == null)
            {
                _instance = new EventLoggerService(client, stream);
            }

            return _instance;
        }

        public void LogEvent(string message)
        {
            try
            {
                if (_stream != null && _client.Connected)
                {
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    _stream.Write(data, 0, data.Length);
                    _stream.Flush();  // Ensure the data is sent immediately
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging event: {ex.Message}");
            }
        }
    }
}
