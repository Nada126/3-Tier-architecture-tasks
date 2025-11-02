using App2.BLL.ModelVM.AccountVM;
using App2.BLL.ModelVM.Employee;
using App2.BLL.ModelVM.ResponseResult;

namespace App2.BLL.Service.Abstraction
{
    public interface IEmployeeService
    {
        Response<List<GetEmployeeVM>> GetActiveEmployee();
        Response<List<GetEmployeeVM>> GetNotActiveEmployee();
        Response<RegisterEmployeeVM>Create(RegisterEmployeeVM model);
        Response<GetEmployeeVM> GetById(string id);
        Response<string> EditEmployee(string id, EditEmployeeVM model);
        Response<string> DeleteEmployee(string id);


    }
}
