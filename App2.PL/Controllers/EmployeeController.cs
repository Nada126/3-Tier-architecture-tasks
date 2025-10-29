using App2.BLL.ModelVM.Employee;
using App2.BLL.Service.Abstraction;
using App2.DAL.Entity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace App2.PL.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;
        private readonly IDepartmentService departmentService;
 
        public EmployeeController(IEmployeeService employeeService,IDepartmentService departmentService)
        {
            this.employeeService = employeeService;
            this.departmentService = departmentService;
        }

        // View employees
        public IActionResult Index()
        {
            var result = employeeService.GetActiveEmployee();
            return View(result);
        }

        // Add new employee
        [HttpGet]
        public IActionResult Create()
        {
            var departments=departmentService.GetAllDepartments();
            ViewBag.Departments = departments;
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateEmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                var result = employeeService.Create(model);
                if (!result.IsHaveErrorOrNot)
                    return RedirectToAction("Index");

                ViewBag.Error = result.ErrorMessage;
            }

            // If failed, reload department list for dropdown
            ViewBag.Departments = new SelectList(departmentService.GetAllDepartments().result, "Id", "Name");
            return View(model);
        }

        // Edit old employee
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var empResponse = employeeService.GetById(id);
            if (empResponse.IsHaveErrorOrNot || empResponse.result == null)
                return NotFound(empResponse.ErrorMessage);

            var departments = departmentService.GetAllDepartments();
            ViewBag.Departments = departments;

            var model = new EditEmployeeVM
            {
                Id = empResponse.result.Id,
                Name = empResponse.result.Name,
                Age = empResponse.result.Age,
                Salary = empResponse.result.Salary,
                DepartmentId = empResponse.result.DepartmentId ?? 0,
                Image = empResponse.result.Image
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditEmployeeVM model)
        {
            if (ModelState.IsValid)
            {
                var result = employeeService.EditEmployee(id, model);
                if (!result.IsHaveErrorOrNot)
                    return RedirectToAction("Index");

                ViewBag.Error = result.ErrorMessage;
            }

            ViewBag.Departments = departmentService.GetAllDepartments();
            return View(model);
        }


        //  Delete old employee
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var empResponse = employeeService.GetById(id);
            if (empResponse.IsHaveErrorOrNot || empResponse.result == null)
                return NotFound(empResponse.ErrorMessage);

            return View(empResponse.result);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = employeeService.DeleteEmployee(id);
            if (!result.IsHaveErrorOrNot)
                return RedirectToAction("Index");

            ViewBag.Error = result.ErrorMessage;
            return View("Delete", employeeService.GetById(id).result);
        }

    }
}
 