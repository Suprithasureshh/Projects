using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    public class ServerSide: IServer
    {
        public void Server()
        {
           
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            serverSocket.Bind(new IPEndPoint(IPAddress.Any, 1234));
            serverSocket.Listen(10);
            Console.WriteLine("Waiting for a client to connect...");

            Socket clientSocket = serverSocket.Accept();
            Console.WriteLine("Client connected.");

           
            while (true)
            {
                byte[] request1 = new byte[1024];
                int requestReceived1 = clientSocket.Receive(request1);
                string messageString = Encoding.ASCII.GetString(request1, 0, requestReceived1);

                Console.WriteLine("Received message from client: {0}", messageString);

                Console.Write("Response to client: ");
                string message = Console.ReadLine();

                string responseString1 = message;
                byte[] response1 = Encoding.ASCII.GetBytes(responseString1);
                clientSocket.Send(response1);

                try
                {
                    byte[] number = new byte[1024];
                    int numberReceived = clientSocket.Receive(number);

                    if (numberReceived == 0)
                    {
                        break;
                    }

                    string expression = Encoding.ASCII.GetString(number, 0, numberReceived);
                    Console.WriteLine("Received message from {0}: {1}", clientSocket.RemoteEndPoint, expression);

                    Console.Write("Response to client: ");

                    int result = (int)new System.Data.DataTable().Compute(expression, null);

                    string responseMessage = result.ToString();
                    byte[] response = Encoding.ASCII.GetBytes(responseMessage);
                    clientSocket.Send(response);

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error occurred: {0}", ex.Message);
                    break;
                }
            }
        }
    }
}