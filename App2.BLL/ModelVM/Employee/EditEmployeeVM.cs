using Microsoft.AspNetCore.Http;

namespace App2.BLL.ModelVM.Employee
{
    public class EditEmployeeVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Age { get; set; }
        public decimal Salary { get; set; }
        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public IFormFile? ImageFile { get; set; }
        public string? Image { get; set; }
    }
}
