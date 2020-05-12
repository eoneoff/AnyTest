using AnyTest.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.IDataRepository
{
    public interface IStudentsRepository:IRepository<Student>
    {
        Task<IEnumerable<Student>> GetStudentPage(int pageNubmer, int pageSize);
    }
}
