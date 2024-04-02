using Client;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        IClient finnaly = new ClientSide();
        finnaly.Client();
    }
}