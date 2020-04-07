
using System;
using System.Collections.Generic;
using System.Text;

namespace AnyTest.IDataRepository
{
    public interface IRepository<T>:IGetter<T>, IPoster<T>, IEditor<T>, IDeleter<T>
    {
    }
}
