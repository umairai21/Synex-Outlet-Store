using System.Net.Sockets;
using System.Net;
using System.Text;
using System;
using System.Threading;

class Program
{
    private static TcpListener listener;
    private static int clientCount = 0;

    static void Main(string[] args)
    {
        // Start the server
        listener = new TcpListener(IPAddress.Any, 5000);
        listener.Start();
        Console.WriteLine("Server started...");

        // Accept clients in separate threads
        while (true)
        {
            try
            {
                var client = listener.AcceptTcpClient();
                Interlocked.Increment(ref clientCount);
                int clientId = clientCount; // Store the client ID for this connection
                Console.WriteLine($"Client {clientId} connected.");

                // Use thread pooling to handle the client
                ThreadPool.QueueUserWorkItem(HandleClient, (client, clientId));
            }
            catch (SocketException ex)
            {
                Console.WriteLine($"Socket error accepting client: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General error accepting client: {ex.Message}");
            }
        }
    }

    private static void HandleClient(object state)
    {
        var (client, clientId) = ((TcpClient, int))state;
        var stream = client.GetStream();
        var buffer = new byte[1024];
        int byteCount;

        Console.WriteLine($"Client {clientId} thread started.");

        try
        {
            while ((byteCount = stream.Read(buffer, 0, buffer.Length)) > 0)
            {
                var request = Encoding.ASCII.GetString(buffer, 0, byteCount);
                Console.WriteLine($"Received from Client {clientId}: {request}");  // Logs the message sent from the client
                var response = Encoding.ASCII.GetBytes($"Hello Client {clientId}, you said: {request}");
                stream.Write(response, 0, response.Length);
            }
        }
        catch (IOException ioEx)
        {
            // Log the IOException which might occur when the client closes the connection unexpectedly
            Console.WriteLine($"Client {clientId} disconnected due to IOException: {ioEx.Message}");
        }
        catch (Exception ex)
        {
            // General exception logging
            Console.WriteLine($"Error with Client {clientId}: {ex.Message}");
        }
        finally
        {
            // Ensure the client connection is closed and resources are cleaned up
            stream.Close();
            client.Close();
            Interlocked.Decrement(ref clientCount);
            Console.WriteLine($"Client {clientId} disconnected.");
        }
    }
}
