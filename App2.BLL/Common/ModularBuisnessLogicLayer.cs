using App2.BLL.Service.Implementation;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using App2.BLL.Mapper;

namespace App2.BLL.Common
{
	public static class ModularBuisnessLogicLayer
	{
		public static IServiceCollection AddBuisnessInBLL(this IServiceCollection services)
		{
			services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));
			return services;

		}

		 
	}
}
