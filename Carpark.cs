using System;

namespace Carpark_TCP
{
    partial class Program
    {
        class Carpark
        {
            public enum TerminalTypes
            {
                SupportTerminal,
                ComplementaryTerminal
            }
            int[] randAmounts = 
                new int[] {1000, 150, 250, 500, 10000, 15000, 5000, 1500, 300, 450,
                    1250, 350, 900, 600, 1300, 2500, 7000, 50000, 4500, 3000};

            public Carpark() // Terminaltypes
            {

            }

            // For now, the terminal will always return "success" and don't do checking
            public byte[] GetNormalFare()
            {
                Console.Write("Get Normal Fare >> ");

                int min = 0;
                int max = 20;
                int randAmountRange = Utils.GetRandNum(min, max);
                int randAmountSelected = randAmounts[randAmountRange];
                string status = string.Empty;
                string sysTime = Utils.GetSystemTime();

                status += Commands.successStatus;
                status += Utils.Byte2StrAscii(Utils.Int2BCD(randAmountSelected, 4));
                status += Utils.Byte2StrAscii(Utils.Str2BCD(sysTime));
                status += Commands.carriageReturn;

                return Utils.Str2Byte(status);
            }
            public byte[] GetComplementaryFare() 
            {
                Console.Write("Get Complementary Fare >> ");

                int min = 0;
                int max = 20;
                int randAmount = Utils.GetRandNum(min, max);
                string status = null;

                status += Commands.successStatus;
                status += Utils.Byte2StrAscii(Utils.Int2BCD(randAmounts[randAmount], 4));
                status += Commands.carriageReturn;

                return Utils.StrAscii2Byte(status);
            }
            public byte[] OpenGate2_0()
            {
                Console.Write("Open Gate 2.0 >> ");
                // For now, just return success
                return Utils.StrAscii2Byte(Commands.successStatusFull);
            }
            public void ShowData(byte[] value)
            {
                string temp = BitConverter.ToString(value);
                temp = temp.Replace("-", string.Empty);
                Console.WriteLine("{0}", temp);
            }
        }
    }
}
