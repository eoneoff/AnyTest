using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.IDataRepository
{
    public interface IEditor<T>
    {
        Task<T> Put(T item);
    }
}
