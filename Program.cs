using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace byteTest
{
    class Program
    {
        public class ComTitles
        {
            public const byte Head = 0xAB;
            public const byte End = 0xFF;
            public const byte Isolator = 0xFD;
            public const byte PacketIsolator = 0xFC;



        }
        public static void Main(string[] args)
        {

            /*byte[] bytes = new byte[3];
            //0,1,2,3
            byte head = 0xFE;
            Console.WriteLine(bytes.Length.ToString());
            Console.ReadKey();/
            /*for(int i = 3; i <= 8; i++)
            {
                Console.WriteLine(i.ToString());
            }
            */
            /*byte sum = 28 + 33 + 22;
            byte[] inc = new byte[] {0xFF,3,0xFF,28,33,22,sum,0xFE};

            byte[] sum2 = new byte[] { 54, 223, 0xFF, 0xEF, 216 };
            byte sum22 = ComputeAdditionChecksum(sum2);

            byte[] fake = new byte[] { 0xEE, 3, 0xFE, 28, 33, 22,44,55, sum, ComTitles.End };
            byte[] inc2 = new byte[] { 0xFF, 5, 0xFF, 54, 223, 0xFF, 0xEF, 216, sum22, 0xFE};*/


            /*byte[] inf = new byte[] { 39, 99, 222, 55 };

            Console.WriteLine("-------");
            writeByteToConsole(inf);

            byte[] conv=ConvertToSyntax(inf);
            writeByteToConsole(conv);
            writeByteToConsole(CheckRevSyntax(conv));
            */
            Console.WriteLine("-------");
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < 500; i++)
            {
                byte[] inf2 = new byte[] { 78, 22, 11, 1, 56, 87, 244, 255, 0 };
                writeByteToConsole(inf2);

                byte[] conv2 = ConvertToSyntax(inf2);
                writeByteToConsole(conv2);
                writeByteToConsole(CheckRevSyntax(conv2));
            }


            /*Console.WriteLine(CheckRevSyntax(fake));
            Console.WriteLine(CheckRevSyntax(inc2));*/
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds + " ms");
            Console.ReadKey();
        }

        public static void writeByteToConsole(byte[] byteArray)
        {
            foreach (byte element in byteArray)
            {
                Console.Write(element + "-");
            }
            Console.WriteLine();
        }
        
        
    }
}
