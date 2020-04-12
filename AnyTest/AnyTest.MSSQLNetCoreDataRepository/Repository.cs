using AnyTest.DbAccess;
using AnyTest.IDataRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AnyTest.MSSQLNetCoreDataRepository
{
    public class Repository<T> : IRepository<T> where T:class
    {
        protected AnyTestDbContext _db;

        public Repository(AnyTestDbContext db) => _db = db;

        public virtual async Task<T> Delete(params object[] key)
        {
            T item = await _db.Set<T>().FindAsync(key);
            if(item != null)
            {
                _db.Remove(item);
                await _db.SaveChangesAsync();
            }

            return item;
        }

        public virtual async Task<bool> Exists(params object[] key)
        {
            var found = await _db.Set<T>().FindAsync(key);
            if(found == null)
            {
                return false;
            }
            else
            {
                _db.Entry<T>(found).State = EntityState.Detached;
                return true;
            }
        }

        public virtual async Task<IEnumerable<T>> Get()
        {
            return await _db.Set<T>().AsNoTracking().ToListAsync();
        }

        public virtual async Task<T> Get(params object[] key)
        {
            var item = await _db.Set<T>().FindAsync(key);
            if(item != null)
            {
                _db.Entry<T>(item).State = EntityState.Detached;
            }

            return item;
        }

        public virtual async Task<T> Post(T item)
        {
            _db.Add(item);
            await _db.SaveChangesAsync();
            _db.Entry(item).State = EntityState.Detached;
            return item;

        }

        public virtual async Task<T> Put(T item)
        {
            _db.Update(item);
            await _db.SaveChangesAsync();
            _db.Entry(item).State = EntityState.Detached;
            return item;
        }
    }
}
