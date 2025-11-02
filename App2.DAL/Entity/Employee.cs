using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace App2.DAL.Entity
{
    public class Employee : IdentityUser
    { 
        public Employee() { }
        public Employee(string name, int age, decimal salary, string image, int? departmentId, string createdUser, string userName)
        {
            Name = name;
            Age = age;
            Salary = salary;
            Image = image;
            CreatedBy = createdUser;
            CreatedOn = DateTime.Now;
            DepartmentId = departmentId;
            UserName = userName;
        }
        public string Name { get; set; }
        public decimal Salary { get;  set; }
        public int Age { get;  set; }
        public string Image { get; set; } ="default.png";
        public int? DepartmentId { get; set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; set; }

        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdatedOn { get; set; }
        public DateTime? DeletedOn { get; set; }
        public string? CreatedBy { get; set; }
        public string? UpdatedBy { get; set; }
        public string? DeletedBy { get; set; }
        public bool IsDeleted { get; set; }

        public bool Update(string? name, int? age, decimal? salary, string? image, int? departmentId, string userModified)
        {
            if (string.IsNullOrWhiteSpace(userModified))
                return false;

            if (!string.IsNullOrEmpty(name)) Name = name;
            if (age.HasValue) Age = age.Value;
            if (salary.HasValue) Salary = salary.Value;
            if (!string.IsNullOrEmpty(image)) Image = image;

            if (departmentId.HasValue)
                DepartmentId = departmentId;
            else if (DepartmentId != null)
                DepartmentId = null;

            LastUpdatedOn = DateTime.Now;
            UpdatedBy = userModified;
            return true;
        }

        public bool ToggleStatus(string deletedUser)
        {
            if (!string.IsNullOrWhiteSpace(deletedUser))
            {
                IsDeleted = !IsDeleted;
                DeletedBy = deletedUser;
                DeletedOn = DateTime.Now;
                return true;
            }
            return false;
        }
    }
}
