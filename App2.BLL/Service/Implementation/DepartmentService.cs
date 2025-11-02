using App2.BLL.ModelVM.Department;
using App2.BLL.ModelVM.ResponseResult;
using App2.DAL.Entity;
using App2.DAL.Repo.Abstraction;
using AutoMapper;

namespace App2.BLL.Service.Implementation
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepo departmentRepo;
        private readonly IMapper mapper;

        public DepartmentService(IDepartmentRepo departmentRepo, IMapper mapper)
        {
            this.departmentRepo = departmentRepo;
            this.mapper = mapper;
        }

        public Response<List<GetDepartmentVM>> GetAllDepartments(bool includeDeleted = false)
        {
            try
            {
                var departments = includeDeleted
                    ? departmentRepo.GetAll()
                    : departmentRepo.GetAll(d => d.IsDeleted == false);

                var mapped = mapper.Map<List<GetDepartmentVM>>(departments);
                return new Response<List<GetDepartmentVM>>(mapped, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<GetDepartmentVM>>(null, ex.Message, true);
            }
        }

        public Response<GetDepartmentVM> GetDepartmentById(int id)
        {
            try
            {
                var department = departmentRepo.GetDepartmentById(id);
                if (department == null)
                    return new Response<GetDepartmentVM>(null, "Department not found", true);

                var mapped = mapper.Map<GetDepartmentVM>(department);
                return new Response<GetDepartmentVM>(mapped, null, false);
            }
            catch (Exception ex)
            {
                return new Response<GetDepartmentVM>(null, ex.Message, true);
            }
        }

        public Response<CreateDepartmentVM> Create(CreateDepartmentVM model)
        {
            try
            {
                var department = new Department( model.Name, model.Area, "Nada");
                var result = departmentRepo.Add(department);

                if (result)
                    return new Response<CreateDepartmentVM>(model, null, false);

                return new Response<CreateDepartmentVM>(model, "Error while saving department", true);
            }
            catch (Exception ex)
            {
                return new Response<CreateDepartmentVM>(null, ex.Message, true);
            }
        }

        public Response<string> EditDepartment(int id, EditDepartmentVM model)
        {
            try
            {
                var department = departmentRepo.GetDepartmentById(id);
                if (department == null)
                    return new Response<string>(null, "Department not found", true);

                var updated = department.Update(model.Name, model.Area, "Nada");
                if (!updated)
                    return new Response<string>(null, "Failed to update department", true);

                var result = departmentRepo.Edit(department);
                if (result)
                    return new Response<string>("Department updated successfully", null, false);

                return new Response<string>(null, "Failed to save department changes", true);
            }
            catch (Exception ex)
            {
                return new Response<string>(null, ex.Message, true);
            }
        }

        public Response<string> DeleteDepartment(int id)
        {
            try
            {
                var result = departmentRepo.ToggleStatus(id);
                if (!result)
                    return new Response<string>(null, "Failed to delete/restore department", true);

                return new Response<string>("Department status changed successfully", null, false);
            }
            catch (Exception ex)
            {
                return new Response<string>(null, ex.Message, true);
            }
        }

    }
}
