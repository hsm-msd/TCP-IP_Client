using System;
using System.Net.Sockets;
using System.Text;
using System.IO;
using System.Net;

class TestClient
{
    static void Main(string[] args)
    {
        if (args.Length == 2)
        {
            string serverAddress = args[0];
            int serverPort;

            if (!IPAddress.TryParse(serverAddress, out IPAddress address))
            {
                Console.WriteLine("Invalid IP address. Please enter a valid IP address.");
                Console.WriteLine("Usage: [TestClient.exe] [Server IP] [Server Port]");
                return;
            }

            if (!int.TryParse(args[1], out serverPort))
            {
                Console.WriteLine("Invalid port number. Please enter a valid port number.");
                Console.WriteLine("Usage: [TestClient.exe] [Server IP] [Server Port]");
                return;
            }

            try
            {
                using (TcpClient client = new TcpClient(serverAddress, serverPort))
                using (NetworkStream stream = client.GetStream())
                {
                    Console.WriteLine("Connected to the logging service.");

                    while (true)
                    {
                        Console.Write("Enter log message (or 'exit' to quit, 'abuse' to send abuse test): ");
                        string message = Console.ReadLine();

                        if (message.ToLower() == "exit")
                            break;

                        if (message.ToLower() == "abuse")
                        {
                            // Send an abuse test (an unfinit quick repetetive messages)
                            Console.WriteLine("Abuse test Started");
                            int i = 0;
                            while (true)
                            {
                                byte[] data = Encoding.UTF8.GetBytes("Abuse test message " + i++);
                                stream.Write(data, 0, data.Length);
                            }
                        }
                        else
                        {
                            byte[] data = Encoding.UTF8.GetBytes(message);
                            stream.Write(data, 0, data.Length);
                            Console.WriteLine("Message sent to the logging service.");
                        }
                    }
                }
            }
            catch (IOException)
            {
                Console.WriteLine("The server has disconnected.");
            }
            catch (SocketException)
            {
                Console.WriteLine("Unable to connect to the server.");
            }
        }
        else
        {
            Console.WriteLine("Please enter a Valid Command\nusage: [TestClient.exe] [Server IP] [Server Port]");
        }
    }
}
