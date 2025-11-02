using App2.BLL.ModelVM.AccountVM;
using App2.BLL.Service.Abstraction;
using App2.DAL.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace App2.PL.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<Employee> userManager;
        private readonly SignInManager<Employee> signInManager;
        private readonly IDepartmentService departmentService;

        public AccountController(UserManager<Employee> userManger, SignInManager<Employee> signInManager, IDepartmentService departmentService)
        {
            this.userManager = userManger;
            this.signInManager = signInManager;
            this.departmentService = departmentService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            var response = departmentService.GetAllDepartments();

            if (response.IsHaveErrorOrNot)
                ViewBag.Departments = Enumerable.Empty<SelectListItem>();
            else
                ViewBag.Departments = new SelectList(response.result, "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                LoadDepartments();
                return View(model);
            }

            string imageName = "default.jpg";

            if (model.Image != null)
            {
                imageName = Guid.NewGuid().ToString() + Path.GetExtension(model.Image.FileName);
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", imageName);
                using var stream = new FileStream(imagePath, FileMode.Create);
                await model.Image.CopyToAsync(stream);
            }

            var user = new Employee(
                model.Name,
                model.Age,
                model.Salary,
                imageName,
                model.DepartmentId,
                "System",
                model.UserName
            );

            var result = await userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                var IsHaveRole = await userManager.IsInRoleAsync(user, "User");
                if (!IsHaveRole)
                {
                    var resultrole = await userManager.AddToRoleAsync(user, "User");
                }
                await signInManager.SignInAsync(user, isPersistent: true);   //Auto Login After Register
                return RedirectToAction("Index", "Employee");
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            LoadDepartments();
            return View(model);
        }

        private void LoadDepartments()
        {
            var response = departmentService.GetAllDepartments();
            ViewBag.Departments = new SelectList(response.result, "Id", "Name");
        }

        [HttpGet]
        public async Task<IActionResult> Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM model)
        {
            var result = await signInManager.PasswordSignInAsync(model.UserName, model.Password,true ,false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Employee");
            }
            else
            {
                ModelState.AddModelError("", "Invalid UserName Or Password");

            }
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Signout()
        {

            await signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
        public IActionResult Index() => View();
    }
}
