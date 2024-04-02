using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    public class ClientSide: IClient
    {
        public void Client()
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1234));
            Console.WriteLine("Connected to server.");
            while (true)
            {   //For message
                Console.Write("Enter Your Message: ");
                string message = Console.ReadLine();

                byte[] request1 = Encoding.ASCII.GetBytes(message);
                clientSocket.Send(request1);

                byte[] response1 = new byte[1024];
                int responseReceived1 = clientSocket.Receive(response1);
                string messageString = Encoding.ASCII.GetString(response1, 0, responseReceived1);

                Console.WriteLine("Server response: {0}", messageString);

                // arithmetic expression
                Console.Write("Enter arithmetic expression: ");
                string expression = Console.ReadLine();

                byte[] request = Encoding.ASCII.GetBytes(expression);
                clientSocket.Send(request);

                byte[] response = new byte[1024];
                int responseReceived = clientSocket.Receive(response);
                string resultString = Encoding.ASCII.GetString(response, 0, responseReceived);

                int result = int.Parse(resultString);
                Console.WriteLine("Result: {0}", result);

                Console.WriteLine("Do you want to continue? (Yes/No)");
                string repeate = Console.ReadLine();

                if (repeate.ToUpper() == "NO")
                {
                    break;
                }
            }

        }
    }
}