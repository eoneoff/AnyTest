using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.IDataRepository
{
    public interface IDeleter<T>
    {
        Task<T> Delete(params object[] key);
    }
}
