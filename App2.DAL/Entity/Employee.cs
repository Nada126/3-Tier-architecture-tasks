
using System.ComponentModel.DataAnnotations.Schema;

namespace App2.DAL.Entity
{
	public class Employee
	{
		protected Employee() { }
		public Employee(string name, int age, decimal salary, string image, int? departmentId,string createdUser)
		{
			Name = name;
			Age = age;
			Salary = salary;
			Image = image;
			CreatedBy = createdUser;
			CreatedOn = DateTime.Now;
			this.DepartmentId = departmentId;
		}
		public int Id { get; private set; }
		public string Name { get; private set; }
		public decimal Salary { get; private set; }
		public int Age { get; private set; }
		public string Image { get; private set; }
		public int? DepartmentId { get; private set; }

        [ForeignKey(nameof(DepartmentId))]
        public Department? Department { get; private set; }
		public DateTime CreatedOn { get; private set; }
		public DateTime? LastUpdatedOn { get; private set; }
		public DateTime? DeletedOn { get; private set; }
		public string? CreatedBy { get; private set; }
		public string? UpdatedBy { get; private set; }
		public string? DeletedBy { get; private set; }
		public bool IsDeleted { get; private set; }

		public bool Update(string? name,int? age,decimal? salary,string? image,int? departmentId,string userModified)
		{
			if (string.IsNullOrEmpty(userModified))
				return false;

			if (!string.IsNullOrEmpty(name))
				Name = name;

			if (age.HasValue)
				Age = age.Value;

			if (salary.HasValue)
				Salary = salary.Value;

			if (!string.IsNullOrEmpty(image))
				Image = image;
 
			if (departmentId.HasValue)
				DepartmentId = departmentId;
			else if (DepartmentId != null && departmentId == null)
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
