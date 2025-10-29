

namespace App2.DAL.Repo.Abstraction
{
    public interface IEmployeeRepo
    {
        //commands
        bool Add(Employee employee);
        bool Edit (Employee employee);
        bool ToggleStatus(int id);    // for deletion

        //queries
        Employee GetEmployeeById(int id);
        List<Employee> GetAll(Expression<Func<Employee,bool>>? Filter=null);
         
    }
}
