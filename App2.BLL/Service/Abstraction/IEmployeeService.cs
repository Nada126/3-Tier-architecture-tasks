using App2.BLL.ModelVM.Employee;
using App2.BLL.ModelVM.ResponseResult;

namespace App2.BLL.Service.Abstraction
{
    public interface IEmployeeService
    {
        Response<List<GetEmployeeVM>> GetActiveEmployee();
        Response<List<GetEmployeeVM>> GetNotActiveEmployee();
        Response<CreateEmployeeVM>Create(CreateEmployeeVM model);
        Response<GetEmployeeVM> GetById(int id);
        Response<string> EditEmployee(int id, EditEmployeeVM model);
        Response<string> DeleteEmployee(int id);


    }
}
