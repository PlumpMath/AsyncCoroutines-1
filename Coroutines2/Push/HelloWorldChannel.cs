using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coroutines2.Push
{
    class HelloWorldChannel : PushAdapterChannel<string>
    {
        protected override async Task Run(IBasicWriteChannel<string> writeChannel)
        {
            await writeChannel.WriteAsync("Hello");
            await writeChannel.WriteAsync("There");
            await writeChannel.WriteAsync("World");
            await writeChannel.WriteAsync((string)null);
        }
    }
}
