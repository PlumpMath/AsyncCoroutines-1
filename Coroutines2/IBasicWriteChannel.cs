using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coroutines2
{
    interface IBasicWriteChannel<T>
    {
        Task WriteAsync(T item);
    }
}
