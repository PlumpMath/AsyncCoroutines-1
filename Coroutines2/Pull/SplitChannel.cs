using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coroutines2.Pull
{
    class SplitChannel : IBasicReadChannel<string>
    {
        IBasicReadChannel<string> _innerChannel;
        string _currentItem;
        int _currentOffset;

        public SplitChannel(IBasicReadChannel<string> innerChannel)
        {
            _innerChannel = innerChannel;
            _currentItem = null;
            _currentOffset = 0;
        }

        public async Task<string> ReadAsync()
        {
            if (_currentItem == null || _currentOffset == _currentItem.Length)
            {
                // get a new string
                _currentItem = await _innerChannel.ReadAsync();

                if (_currentItem == null)
                    return null;

                _currentOffset = 0;
            }

            char c = _currentItem[_currentOffset];
            _currentOffset++;

            return c.ToString();
        }
    }
}
