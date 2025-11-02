
namespace App2.BLL.ModelVM.Employee
{
    public class GetEmployeeVM
    {
        public string Id { get; set; } =string.Empty;
        public string Name { get;  set; }
		public int Age { get; set; }
		public decimal Salary { get; set; }
		public string? Image { get; set; }

		public int? DepartmentId { get; set; }
		public string? DepartmentName { get; set; }

	}
}
