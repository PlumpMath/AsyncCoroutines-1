using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coroutines2;

namespace Coroutines2
{
    class Program
    {
        static IBasicReadChannel<string> MakePull()
        {
            return new Pull.DumpChannel("Outer dump",
                new Pull.SplitChannel(
                    new Pull.DumpChannel("Inner dump",
                        new Pull.HelloWorldChannel())));
        }

        static IBasicReadChannel<string> MakePush()
        {
            return new Push.DumpChannel("Outer dump",
                new Push.SplitChannel(
                    new Push.DumpChannel("Inner dump",
                        new Push.HelloWorldChannel())));
        }

        static async Task Run(IBasicReadChannel<string> channel)
        {
            Console.WriteLine("Processing channel items");

            while (true)
            {
                var item = await channel.ReadAsync();
                if (item == null)
                    break;
            }

            Console.WriteLine("Done with channel");
        }

        static void Main(string[] args)
        {
//            var t = Run(MakePull());
            var t = Run(MakePush());

            t.Wait();

            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}
