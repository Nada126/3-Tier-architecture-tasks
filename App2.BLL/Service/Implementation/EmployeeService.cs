using App2.BLL.Helper;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace App2.BLL.Service.Implementation
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepo employeeRepo;
        private readonly IMapper mapper;

        public EmployeeService(IEmployeeRepo employeeRepo, IMapper mapper)
        {
            this.employeeRepo = employeeRepo;
            this.mapper = mapper;
        }

        //Add new employee
        public Response<CreateEmployeeVM> Create(CreateEmployeeVM model)
        {
            try
            {
                string defaultImage = "default.jpg";
                string imageToUse = defaultImage;

                if (model.ImageFile != null)
                {
                    imageToUse = Upload.UploadFile("Files", model.ImageFile);
                }

                var emp = new Employee(
                    model.Name,
                    model.Age,
                    model.Salary,
                    imageToUse,
                    model.DepartmentId,
                    "Nada"
                );

                var result = employeeRepo.Add(emp);

                if (result)
                    return new Response<CreateEmployeeVM>(model, null, false);

                return new Response<CreateEmployeeVM>(model, "There was a problem saving the employee.", true);
            }
            catch (Exception ex)
            {
                return new Response<CreateEmployeeVM>(null, ex.Message, true);
            }
        }


        // Get all active employees
        public Response<List<GetEmployeeVM>> GetActiveEmployee()
        {
            try
            {
                var employees = employeeRepo.GetAll(e => e.IsDeleted==false);

                var result = employees.Select(emp => new GetEmployeeVM
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Age = emp.Age,
                    Salary = emp.Salary,
                    Image = emp.Image,
                    DepartmentId = emp.DepartmentId,
                    DepartmentName = emp.Department != null ? emp.Department.Name : "No Department"
                }).ToList();

                return new Response<List<GetEmployeeVM>>(result, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<GetEmployeeVM>>(null, ex.Message, true);
            }
        }

        // Get all deleted employees
        public Response<List<GetEmployeeVM>> GetNotActiveEmployee()
        {
            try
            {
                var employees = employeeRepo.GetAll(e => e.IsDeleted==true);
                var result = employees.Select(emp => new GetEmployeeVM
                {
                    Id = emp.Id,
                    Name = emp.Name,
                    Age = emp.Age,
                    Salary = emp.Salary,
                    Image = emp.Image,
                    DepartmentId = emp.DepartmentId,
                    DepartmentName = emp.Department != null ? emp.Department.Name : "No Department"
                }).ToList();
                return new Response<List<GetEmployeeVM>>(result, null, false);
            }
            catch (Exception ex)
            {
                return new Response<List<GetEmployeeVM>>(null, ex.Message, true);
            }
        }

        // Get employee by ID
        public Response<GetEmployeeVM> GetById(int id)
        {
            try
            {
                var emp = employeeRepo.GetEmployeeById(id);
                if (emp == null)
                    return new Response<GetEmployeeVM>(null, "Employee not found", true);

                var map = mapper.Map<GetEmployeeVM>(emp);
                return new Response<GetEmployeeVM>(map, null, false);
            }
            catch (Exception ex)
            {
                return new Response<GetEmployeeVM>(null, ex.Message, true);
            }
        }

        // Edit employee
        public Response<string> EditEmployee(int id, EditEmployeeVM model)
        {
            try
            {
                var employee = employeeRepo.GetEmployeeById(id);
                if (employee == null)
                    return new Response<string>(null, "Employee not found", true);

                string imageName = employee.Image ?? "default.jpg";

                if (model.ImageFile != null)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files");
                    if (!Directory.Exists(uploadsFolder))
                        Directory.CreateDirectory(uploadsFolder);

                    imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.ImageFile.FileName);
                    string filePath = Path.Combine(uploadsFolder, imageName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        model.ImageFile.CopyTo(stream);
                    }
                }

                var updated = employee.Update(
                    model.Name,
                    model.Age,
                    model.Salary,
                    imageName,
                    model.DepartmentId,
                    "Nada"
                );

                if (!updated)
                    return new Response<string>(null, "Failed to update employee", true);

                employeeRepo.Edit(employee);
                return new Response<string>("Employee updated successfully", null, false);
            }
            catch (Exception ex)
            {
                return new Response<string>(null, ex.Message, true);
            }
        }



        // Delete employee (soft delete)
        public Response<string> DeleteEmployee(int id)
        {
            try
            {
                var result = employeeRepo.ToggleStatus(id);
                if (!result)
                    return new Response<string>(null, "Failed to delete/restore employee", true);

                return new Response<string>("Employee status changed successfully", null, false);
            }
            catch (Exception ex)
            {
                return new Response<string>(null, ex.Message, true);
            }
        }

    }
}
