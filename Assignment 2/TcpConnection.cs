using System;
using System.Net.Sockets;
using System.Text;

namespace Assignment_2
{
    public class TcpConnection
    {
        private TcpClient client;
        private NetworkStream stream;

        // Method to connect the client to the server
        public void ConnectToServer(string ipAddress = "127.0.0.1", int port = 5000)
        {
            try
            {
                client = new TcpClient(ipAddress, port);  // Connect to the server at specified IP and port
                stream = client.GetStream();
                LogEvent("Client connected.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Could not connect to server: {ex.Message}");
            }
        }

        // Method to log events by sending them to the server
        public void LogEvent(string message)
        {
            try
            {
                if (stream != null && client.Connected)
                {
                    byte[] data = Encoding.ASCII.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    stream.Flush();  // Ensure the data is sent immediately
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging event: {ex.Message}");
            }
        }

        // Method to close the client connection
        public void CloseClientConnection()
        {
            try
            {
                stream?.Close();
                client?.Close();
                Console.WriteLine("Client connection closed.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error closing connection: {ex.Message}");
            }
        }
    }
}
