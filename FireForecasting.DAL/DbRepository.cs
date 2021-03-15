using FireForecasting.DAL.Context;
using FireForecasting.DAL.Entityes.Base;
using FireForecasting.DAL.Entityes.Departments;
using FireForecasting.DAL.Entityes.Incidents;
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
            _departmentDB.Remove(new T { Id = id });
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

    class DivisionRepository : DbRepository<Division>
    {
        public override IQueryable<Division> Items => base.Items.Include(item => item.Department);

        public DivisionRepository(DepartmentDB departmentDB) : base(departmentDB) { }
    }

    class EmployeeRepository : DbRepository<Employee>
    {
        public override IQueryable<Employee> Items => base.Items
            .Include(item => item.Division.Department);

        public EmployeeRepository(DepartmentDB departmentDB) : base(departmentDB) { }
    }
    class FireRepository : DbRepository<Fire>
    {
        public override IQueryable<Fire> Items => base.Items
            .Include(item => item.Division)
            .Include(item => item.Employee);

        public FireRepository(DepartmentDB departmentDB) : base(departmentDB) { }
    }

    class DepartmentRepository : DbRepository<Department>
    {
        public override IQueryable<Department> Items => base.Items
            .Include(item => item.Divisions);

        public DepartmentRepository(DepartmentDB departmentDB) : base(departmentDB) { }
    }
}
