 
namespace App2.DAL.Entity
{
	public class Department
	{
		protected Department() { }

		public Department(int id,string name, double area,string createdUser)
		{
            Id = id;
			Name = name;
			Area = area;
            CreatedBy = createdUser;
            CreatedOn = DateTime.Now;
        }

		public int Id { get; private set; }
		public string Name { get; private set; }
		public double Area { get; private set; }
		public List<Employee> Employees { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime? LastUpdatedOn { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? CreatedBy { get; private set; }
        public string? UpdatedBy { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        public bool Update(string? name, double? area ,string userModified)
        {
            if (string.IsNullOrEmpty(userModified))
                return false;

            if (!string.IsNullOrEmpty(name))
                Name = name;

            if (area.HasValue)
                Area = area.Value;

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
