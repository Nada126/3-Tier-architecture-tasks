using App2.DAL.Repo.Implemetation;
using Microsoft.Extensions.DependencyInjection;

namespace App2.DAL.Common
{
	public static class ModularDataAccessLayer
	{
		public static IServiceCollection AddBuisnessInDAL(this IServiceCollection services) 
		{
			services.AddScoped<IEmployeeRepo, EmployeeRepo>();
			services.AddScoped<IDepartmentRepo, DepartmentRepo>();
			return services;

		}
	}
}
