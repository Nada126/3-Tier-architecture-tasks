namespace App2.BLL.ModelVM.Department
{
    public class GetDepartmentVM
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public double Area { get; set; }
        public List<GetEmployeeVM>? Employees { get; set; }
    }
}
