using System;
using System.Collections.Generic;
using System.Linq;
using Store.Core.DataLayer.Contracts;
using Store.Core.EntityLayer.HumanResources;

namespace Store.Core.DataLayer.Repositories
{
    public class HumanResourcesRepository : Repository, IHumanResourcesRepository
    {
        public HumanResourcesRepository(IUserInfo userInfo, StoreDbContext dbContext)
            : base(userInfo, dbContext)
        {
        }

        public IEnumerable<Employee> GetEmployees(Int32 pageSize, Int32 pageNumber)
        {
            var query = DbContext.Set<Employee>().AsQueryable();

            return pageSize > 0 && pageNumber > 0 ? query.Skip((pageNumber - 1) * pageSize).Take(pageSize) : query;
        }

        public Employee GetEmployee(Employee entity)
        {
            return DbContext.Set<Employee>().FirstOrDefault(item => item.EmployeeID == entity.EmployeeID);
        }

        public void AddEmployee(Employee entity)
        {
            DbContext.Set<Employee>().Add(entity);

            DbContext.SaveChanges();
        }

        public void UpdateEmployee(Employee changes)
        {
            var entity = GetEmployee(changes);

            if (entity != null)
            {
                entity.FirstName = changes.FirstName;
                entity.MiddleName = changes.MiddleName;
                entity.LastName = changes.LastName;
                entity.BirthDate = changes.BirthDate;

                DbContext.SaveChanges();
            }
        }

        public void DeleteEmployee(Employee entity)
        {
            DbContext.Set<Employee>().Remove(entity);

            DbContext.SaveChanges();
        }
    }
}
