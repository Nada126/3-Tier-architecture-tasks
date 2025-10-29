

using Microsoft.AspNetCore.Http;

namespace App2.BLL.ModelVM.Employee
{
    public class CreateEmployeeVM
    {
        public string Name { get;  set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public int Age { get;  set; }
        public string? Image { get; set; }
        public IFormFile? ImageFile { get; set; }
    }
}
