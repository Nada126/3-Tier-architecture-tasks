
namespace App2.DAL.Repo.Implemetation
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly App2DbContext Db;
        public EmployeeRepo(App2DbContext Db) { this.Db = Db; }
        public bool Add(Employee employee)
        {
            try
            {
                var result = Db.Employees.Add(employee);
                Db.SaveChanges();
                if (result.Entity.Id > 0)    //new employee gets an auto-generated ID
                    return true;
                return false; 
            }
            catch (Exception ex)
            {
                throw;
            }
        }

		public bool Edit(Employee newEmployee)
		{
			try
			{
				var oldEmployee = Db.Employees.FirstOrDefault(a => a.Id == newEmployee.Id);
				if (oldEmployee != null)
				{
					var result = oldEmployee.Update(
						newEmployee.Name,
						newEmployee.Age,
						newEmployee.Salary,
						newEmployee.Image,
						newEmployee.DepartmentId,
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

        //public List<Employee> GetActiveEmployees()
        //{
        //    throw new NotImplementedException();
        //}

        public List<Employee> GetAll(Expression<Func<Employee, bool>>? Filter = null)
        {
            try
            {
                IQueryable<Employee> query = Db.Employees.Include(e => e.Department); 

                if (Filter != null)
                    query = query.Where(Filter);

                return query.ToList();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public Employee GetEmployeeById(int id)
        {
            try
            {
                var employee = Db.Employees.Include(e => e.Department).FirstOrDefault(e => e.Id == id);
                return employee;
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
                var OldEmployee = Db.Employees.Where(a => a.Id == id).FirstOrDefault();
                if (OldEmployee != null)
                {
                    var result = OldEmployee.ToggleStatus("Nada");
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
