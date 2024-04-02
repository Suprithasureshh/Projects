using Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        IServer finnaly = new ServerSide();
        finnaly.Server();
    }

}