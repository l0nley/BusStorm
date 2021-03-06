﻿using System.Net.Sockets;
using BusStorm.Logging;

namespace BusStorm.Sockets
{
  public class BusClientSocket<T> : BusClientConnection<T> where T : BusStormMessageBase
  {
    private readonly string _host;
    private readonly int _port;

    public BusClientSocket(string host, int port, IBusProtocolFactory<T> factory)
      : base(factory)
    {
      Tracer.Log("Client socket created with {0} {1}", host, port);
      _host = host;
      _port = port;
      Socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
    }

    public void Connect()
    {
      Tracer.Log("Client socket Connect fired");
      Socket.Connect(_host, _port);
      Connected = true;
      StartReceiving();
    }

    public void Disconnect(bool reuse = false)
    {
      Tracer.Log("Client socket disconnect fired {0}", reuse);
      Socket.Disconnect(reuse);
    }
  }
}