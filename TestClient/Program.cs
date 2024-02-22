using System;
using System.Net.Sockets;
using System.Text;

class TestClient
{
    static void Main(string[] args)
    {

        if (args.Length == 1)
        {
            string serverAddress = args[0];
            int serverPort = 5300;

            using (TcpClient client = new TcpClient(serverAddress, serverPort))
            using (NetworkStream stream = client.GetStream())
            {
                Console.WriteLine("Connected to the logging service.");

                while (true)
                {
                    Console.Write("Enter log message (or 'exit' to quit): ");
                    string message = Console.ReadLine();

                    if (message.ToLower() == "exit")
                        break;

                    byte[] data = Encoding.UTF8.GetBytes(message);
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Message sent to the logging service.");
                }
            }
        }
        else
        {
            Console.WriteLine("Please enter a Valid Command\nusage: [TestClient.exe] [Server ZeroTier IP]");
        }

    }
}
