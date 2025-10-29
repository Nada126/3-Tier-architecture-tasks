using App2.BLL.ModelVM.Department;
using App2.DAL.Entity;

namespace App2.BLL.Service.Abstraction
{
    public interface IDepartmentService
    {
        Response<List<GetDepartmentVM>> GetAllDepartments(bool includeDeleted = false);
        Response<GetDepartmentVM> GetDepartmentById(int id);
        Response<CreateDepartmentVM> Create(CreateDepartmentVM model);
        Response<string> EditDepartment(int id, EditDepartmentVM model);
        Response<string> DeleteDepartment(int id);
    }
}
