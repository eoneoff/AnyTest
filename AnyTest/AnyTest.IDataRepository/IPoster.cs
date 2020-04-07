using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.IDataRepository
{
    public interface IPoster<T>
    {
        Task<T> Post(T item);
    }
}
