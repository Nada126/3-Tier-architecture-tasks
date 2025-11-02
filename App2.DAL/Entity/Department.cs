namespace App2.DAL.Entity
{
    public class Department
    {
        protected Department()
        {
            Employees = new List<Employee>();
        }
        public Department(string name, string area, string createdUser)
        {
            Name = name;
            Area = area;
            CreatedBy = createdUser;
            CreatedOn = DateTime.Now;
            Employees = new List<Employee>();
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string Area { get; private set; }
        public List<Employee> Employees { get; private set; }
        public DateTime CreatedOn { get; private set; } = DateTime.Now;
        public DateTime? LastUpdatedOn { get; private set; }
        public DateTime? DeletedOn { get; private set; }
        public string? CreatedBy { get; private set; }
        public string? UpdatedBy { get; private set; }
        public string? DeletedBy { get; private set; }
        public bool IsDeleted { get; private set; }

        public bool Update(string? name, string? area, string userModified)
        {
            if (string.IsNullOrEmpty(userModified))
                return false;

            if (!string.IsNullOrEmpty(name))
                Name = name;

            if (!string.IsNullOrEmpty(area))
                Area = area;

            LastUpdatedOn = DateTime.Now;
            UpdatedBy = userModified;
            return true;
        }

        public bool ToggleStatus(string deletedUser)
        {
            if (string.IsNullOrWhiteSpace(deletedUser))
                return false;

            IsDeleted = !IsDeleted;
            DeletedBy = deletedUser;
            DeletedOn = IsDeleted ? DateTime.Now : null;  

            return true;
        }
    }
}
