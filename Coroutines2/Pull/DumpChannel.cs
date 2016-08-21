using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coroutines2.Pull
{
    class DumpChannel : IBasicReadChannel<string>
    {
        IBasicReadChannel<string>_innerChannel;
        string _info;

        public DumpChannel(string info, IBasicReadChannel<string> innerChannel)
        {
            _info = info;
            _innerChannel = innerChannel;
        }

        public async Task<string> ReadAsync()
        {
            var item = await _innerChannel.ReadAsync();

            if (item == null)
                return null;

            Console.WriteLine("{0}: {1}", _info, item);
            return item;
        }
    }
}
