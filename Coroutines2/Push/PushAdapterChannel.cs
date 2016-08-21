using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coroutines2.Push
{
    abstract class PushAdapterChannel<T> : IBasicReadChannel<T>
    {
        TaskCompletionSource<T> _readTask;
        TaskCompletionSource<bool> _writeTask;

        class AdapterWriteChannel : IBasicWriteChannel<T>
        {
            PushAdapterChannel<T> _adapterChannel;

            public AdapterWriteChannel(PushAdapterChannel<T> adapterChannel)
            {
                _adapterChannel = adapterChannel;
            }
 
            public Task WriteAsync(T item)
            {
                return _adapterChannel.WriteAsync(item);
            }
        }

        public PushAdapterChannel()
        {
            AdapterWriteChannel writeChannel = new AdapterWriteChannel(this);
            _writeTask = new TaskCompletionSource<bool>();

            DoIt(writeChannel);
        }

        public async Task<T> ReadAsync()
        {
            _readTask = new TaskCompletionSource<T>();

            // Unblock the coroutine to produce another value
            _writeTask.SetResult(true);

            // Wait for it to finish
            var item = await _readTask.Task;

            return item;
        }

        private async Task WriteAsync(T item)
        {
            _writeTask = new TaskCompletionSource<bool>();

            // Unblock the coroutine 
            _readTask.SetResult(item);

            // Wait for it to finish
            await _writeTask.Task;
        }

        private async void DoIt(IBasicWriteChannel<T> writeChannel)
        {
            // Wait for first read before we kick off processing
            await _writeTask.Task;

            await Run(writeChannel);
        }

        protected abstract Task Run(IBasicWriteChannel<T> writeChannel);
    }
}
