
using App2.DAL.Entity;
using System.Linq;

namespace App2.DAL.Repo.Implemetation
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly App2DbContext Db;

        public DepartmentRepo(App2DbContext context)
        {
            this.Db = context;
        }

        public bool Add(Department department)
        {
            try
            {
                var result = Db.Departments.Add(department);
                Db.SaveChanges();
                if (result.Entity.Id > 0)     
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Edit(Department newDepartment)
        {
            try
            {
                var oldDepartment = Db.Departments.FirstOrDefault(a => a.Id == newDepartment.Id);
                if (oldDepartment != null)
                {
                    var result = oldDepartment.Update(
                        newDepartment.Name,
                        newDepartment.Area,
                        "Nada"
                    );

                    if (result)
                    {
                        Db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Department> GetAll(Expression<Func<Department, bool>>? Filter = null)
        {
            try
            {
                IQueryable<Department> query = Db.Departments.Include(d => d.Employees);

                if (Filter != null)
                    query = query.Where(Filter);

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Department GetDepartmentById(int id)
        {
            try
            {
                var department = Db.Departments
                    .Include(d => d.Employees)
                    .FirstOrDefault(d => d.Id == id);
                return department;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool ToggleStatus(int id)
        {
            try
            {
                var OldDepartment = Db.Departments.Where(a => a.Id == id).FirstOrDefault();
                if (OldDepartment != null)
                {
                    var result = OldDepartment.ToggleStatus("Nada");
                    if (result)
                    {
                        Db.SaveChanges();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
