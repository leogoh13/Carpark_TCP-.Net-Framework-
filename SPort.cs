using System;
using System.IO.Ports;

namespace Carpark_TCP
{
    class SPort
    {
        SerialPort serialPort;
        XML xml;
        public SPort(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
        {
            this.serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
            this.xml = new XML("Config.xml");
        }

        public void ConnectPort()
        {
            try
            {
                serialPort.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} was caught at ConnectPort()", e);
                throw;
            }
        }
        public void ReadData(out string recvDataStr)
        {
            byte[] recvDataByte = new byte[100];

            try
            {
                serialPort.Read(recvDataByte, 0, recvDataByte.Length);
                recvDataStr = Utils.Byte2StrHex(recvDataByte);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} was caught at ReadData()", e);
                throw;
            }
        }
        public void SendData(string sendDataStr)
        {
            byte[] sendDataByte = Utils.StrAscii2Byte(sendDataStr);
            
            try
            {
                serialPort.Write(sendDataByte, 0, sendDataByte.Length);
            }
            catch (Exception e)
            {
                Console.WriteLine("{0} was caught at SendData()", e);
                throw;
            }
        }

    }
}
