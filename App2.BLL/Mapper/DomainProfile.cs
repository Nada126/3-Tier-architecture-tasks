
using App2.BLL.ModelVM.Department;
using App2.DAL.Entity;
using AutoMapper;
using App2.BLL.ModelVM.Employee;


namespace App2.BLL.Mapper
{
	public class DomainProfile:Profile
	{
		public DomainProfile() 
		{
            CreateMap<Employee, GetEmployeeVM>()
            .ForMember(dest => dest.DepartmentName,
                       opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : "No Department")).ReverseMap();
            CreateMap<CreateEmployeeVM, Employee>();

			CreateMap<Department, GetDepartmentVM>().ForMember(dest => dest.Employees, opt => opt.MapFrom(src => src.Employees)).ReverseMap();
            CreateMap<CreateDepartmentVM, Department>();
            CreateMap<EditDepartmentVM, Department>().ReverseMap();
        }

	}
}
