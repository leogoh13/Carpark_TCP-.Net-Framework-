using System;

namespace Carpark_TCP
{
    partial class Program
    {
        static void Main(string[] args)
        {
            string filename = "Config.xml";
            byte[] sendData_byte = null;

            XML xml = new XML(filename);
            string address = xml.GetStrXML("/config/tcpip/address");
            int port = xml.GetIntXML("/config/tcpip/port");

            Carpark cp = new Carpark();
            TCPIP tcpip = new TCPIP(address, port);

            Console.WriteLine("Running at IP:" + address + " Port: " + port.ToString() + "\n");

            while (true)
            {
                Console.WriteLine("Host Server: Listening");
                string recvData = Utils.Byte2StrAscii(tcpip.StartListen());
                // Can be better
                if (recvData.Contains(Commands.complementaryFareCommand))
                    sendData_byte = cp.GetComplementaryFare();
                else if (recvData.Contains(Commands.normalFareCommand))
                    sendData_byte = cp.GetNormalFare();

                if (recvData.Contains(Commands.openGateCommand2_0))
                    sendData_byte = cp.OpenGate2_0();

                if (sendData_byte != null)
                    tcpip.SendData(sendData_byte);

                tcpip.StopListen();
            }
        }

        static class Commands
        {
            // Commands from terminal
            public const string complementaryFareCommand = "\x40\x39\x30\x32";
            public const string normalFareCommand = "\x40\x39\x30\x31";
            public const string openGateCommand2_0 = "\x40\x39\x31\x32";

            // Reply to commands
            public const string successStatus = "\x3E";
            public const string successStatusFull = "\x3E\x0D";
            public const string failedStatusFull = "\x3F\x0D";

            //
            public const string carriageReturn = "\x0D";
        }
    }
}
