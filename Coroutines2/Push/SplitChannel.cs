using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coroutines2.Push
{
    class SplitChannel : PushAdapterChannel<string>
    {
        IBasicReadChannel<string> _innerChannel;

        public SplitChannel(IBasicReadChannel<string> innerChannel)
        {
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

                foreach (char c in item)
                {
                    await writeChannel.WriteAsync(c.ToString());
                }
            }
        }
    }
}
