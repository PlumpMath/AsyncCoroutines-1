using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coroutines2.Pull
{
    class HelloWorldChannel : IBasicReadChannel<string>
    {
        int _state;

        public HelloWorldChannel()
        {
            _state = 0;
        }

        public Task<string> ReadAsync()
        {
            switch (_state)
            {
                case 0:
                    _state++;
                    return Task.FromResult("Hello");

                case 1:
                    _state++;
                    return Task.FromResult("There");

                case 2:
                    _state++;
                    return Task.FromResult("World");

                default:
                    return Task.FromResult((string)null);   // indicate end of stream
            }
        }
    }
}
