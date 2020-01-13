using System;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Carpark_TCP
{
    partial class Program
    {
        class TCPIP
        {
            TcpListener tcpListener;
            IPEndPoint localEndPoint;
            Socket clientSocket;
            Socket listener;
            IPAddress IP;
            string address;
            int port;

            public TCPIP(string address, int port)
            {
                this.address = address;
                this.port = port;

                IP = IPAddress.Parse(address);
                tcpListener = new TcpListener(IP, port);
                localEndPoint = new IPEndPoint(IP, port);
            }
            public byte[] StartListen()
            {
                byte[] data = new byte[100];
                listener = new Socket(IP.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                
                try
                {
                    listener.Bind(localEndPoint);
                    listener.Listen(10);
                    
                    while (true)
                    {
                        clientSocket = listener.Accept();
                        int numByte = clientSocket.Receive(data);
                        string strData = Encoding.ASCII.GetString(data);
                        foreach (char a in strData)
                            if (a == '\0')
                                break;

                        RecvData(data);
                        break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error..... " + e.StackTrace);
                }
                return data;
            }
            public void StopListen()
            {
                try
                {
                    listener.Close();
                    tcpListener.Stop();
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error..... " + e.StackTrace);
                }
            }
            public void RecvData(byte[] message)
            {
                int lenOfEOF;
                string temp = BitConverter.ToString(message);
                temp = temp.Replace("-", string.Empty);
                lenOfEOF = temp.LastIndexOf("0D") + 2;
                temp = temp.Substring(0, lenOfEOF);
                Console.WriteLine("Receiving Data >> {0}", temp);
            }
            public void SendData(byte[] message)
            {
                try
                {
                    clientSocket.Send(message);
                    string temp = Utils.Byte2StrHex(message);
                    Console.WriteLine("Sending Data >> {0}\n\n", temp);
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error..... " + e.StackTrace);
                }
            }
        }
    }
}
