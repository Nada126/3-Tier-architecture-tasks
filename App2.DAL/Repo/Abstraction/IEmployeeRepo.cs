

namespace App2.DAL.Repo.Abstraction
{
    public interface IEmployeeRepo
    {
        //commands
        bool Add(Employee employee);
        bool Edit (Employee employee);
        bool ToggleStatus(string id);    // for deletion

        //queries
        Employee GetEmployeeById(string id);
        List<Employee> GetAll(Expression<Func<Employee,bool>>? Filter=null);
         
    }
}
