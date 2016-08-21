using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coroutines2.Push
{
    class DumpChannel : PushAdapterChannel<string>
    {
        IBasicReadChannel<string> _innerChannel;
        string _info;

        public DumpChannel(string info, IBasicReadChannel<string> innerChannel)
        {
            _info = info;
            _innerChannel = innerChannel;
        }

        protected override async Task Run(IBasicWriteChannel<string> writeChannel)
        {
            while (true)
            {
                var item = await _innerChannel.ReadAsync();

                if (item == null)
                {
                    await writeChannel.WriteAsync(null);
                    return;
                }

                Console.WriteLine("{0}: {1}", _info, item);

                await writeChannel.WriteAsync(item);
            }
        }
    }
}
