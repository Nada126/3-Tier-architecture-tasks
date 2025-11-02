using App2.DAL.Database;
using App2.DAL.Entity;
using App2.DAL.Repo.Abstraction;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace App2.DAL.Repo.Implemetation
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly App2DbContext Db;
        public EmployeeRepo(App2DbContext Db)
        {
            this.Db = Db;
        }

        public bool Add(Employee employee)
        {
            try
            {
                var result = Db.Users.Add(employee);
                Db.SaveChanges();

                return !string.IsNullOrEmpty(result.Entity.Id);
            }
            catch
            {
                return false;
            }
        }

        public bool Edit(Employee newEmployee)
        {
            try
            {
                var oldEmployee = Db.Users.FirstOrDefault(e => e.Id == newEmployee.Id);
                if (oldEmployee == null)
                    return false;

                bool updated = oldEmployee.Update(
                    newEmployee.Name,
                    newEmployee.Age,
                    newEmployee.Salary,
                    newEmployee.Image,
                    newEmployee.DepartmentId,
                    "Nada" 
                );

                if (updated)
                {
                    Db.SaveChanges();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public List<Employee> GetAll(Expression<Func<Employee, bool>>? filter = null)
        {
            IQueryable<Employee> query = Db.Users.Include(e => e.Department);

            if (filter != null)
                query = query.Where(filter);

            return query.ToList();
        }

        public Employee GetEmployeeById(string id)
        {
            return Db.Users
                     .Include(e => e.Department)
                     .FirstOrDefault(e => e.Id == id);
        }

        public bool ToggleStatus(string id)
        {
            try
            {
                var employee = Db.Users.FirstOrDefault(e => e.Id == id);
                if (employee == null)
                    return false;

                bool toggled = employee.ToggleStatus("Nada");  

                if (toggled)
                {
                    Db.SaveChanges();
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
