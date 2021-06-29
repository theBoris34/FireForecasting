using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Base;
using FireForecasting.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace FireForecasting.DAL
{
    internal class DbRepository<T> : IRepository<T> where T : Entity, new()
    {
        private readonly DepartmentDB _departmentDB;
        private readonly DbSet<T> _Set;

        public bool AutoSaveChanges { get; set; } = true;

        public DbRepository(DepartmentDB departmentDB)
        {
            _departmentDB = departmentDB;
            _Set = departmentDB.Set<T>();
        }

        public virtual IQueryable<T> Items => _Set;

        public T Add(T item)
        {
            if (item is null) throw new ArgumentException(nameof(item));
            _departmentDB.Entry(item).State = EntityState.Added;
            if(AutoSaveChanges)
                _departmentDB.SaveChanges();
            return item;
        }

        public async Task<T> AddAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentException(nameof(item));
            _departmentDB.Entry(item).State = EntityState.Added;
            if (AutoSaveChanges)
                await _departmentDB.SaveChangesAsync().ConfigureAwait(false);
            return item;
        }

        public T Get(int id)
        {
            return Items.SingleOrDefault(item => item.Id == id);
        }

        public async Task<T> GetAsync(int id, CancellationToken Cancel = default) => await Items.
            SingleOrDefaultAsync(item => item.Id == id, Cancel)
            .ConfigureAwait(false);

        public void Remove(int id)
        {
            //TODO: каскадное удаление?

            var item = _Set.Local.FirstOrDefault(i => i.Id == id) ?? new T { Id = id };
            _departmentDB.Entry(item).State = EntityState.Deleted;
            //_departmentDB.Remove(item);
            if (AutoSaveChanges)
                 _departmentDB.SaveChanges();

        }

        public async Task RemoveAsync(int id, CancellationToken Cancel = default)
        {
            _departmentDB.Remove(new T { Id = id });
            if (AutoSaveChanges)
               await  _departmentDB.SaveChangesAsync().ConfigureAwait(false);
        }

        public void Update(T item)
        {
            if (item is null) throw new ArgumentException(nameof(item));
            _departmentDB.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                _departmentDB.SaveChanges();
        }

        public async Task UpdateAsync(T item, CancellationToken Cancel = default)
        {
            if (item is null) throw new ArgumentException(nameof(item));
            _departmentDB.Entry(item).State = EntityState.Modified;
            if (AutoSaveChanges)
                _departmentDB.SaveChanges();
        }
    }
}
