using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App2.BLL.ModelVM.AccountVM
{
   public class RegisterEmployeeVM
    {
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int Age { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public int DepartmentId { get; set; }
        public IFormFile? Image { get; set; }
    }
}
