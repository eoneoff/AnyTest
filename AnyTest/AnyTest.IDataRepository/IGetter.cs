using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.IDataRepository
{
    public interface IGetter<T>
    {
        Task<IEnumerable<T>> Get();
        Task<T> Get(params object[] key);
        Task<bool> Exists(params object[] key);
    }
}
