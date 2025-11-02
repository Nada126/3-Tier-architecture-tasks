using App2.BLL.ModelVM.Employee;
using App2.BLL.Service.Abstraction;
using App2.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace App2.PL.Controllers
{
    [Authorize(Roles = "User")]
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
        //[HttpGet]
        //public IActionResult Create()
        //{
        //    var departments=departmentService.GetAllDepartments();
        //    ViewBag.Departments = departments;
        //    return View();
        //}
        //[HttpPost]
        //public IActionResult Create(CreateEmployeeVM model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var result = employeeService.Create(model);
        //        if (!result.IsHaveErrorOrNot)
        //            return RedirectToAction("Index");

        //        ViewBag.Error = result.ErrorMessage;
        //    }

        //    // If failed, reload department list for dropdown
        //    ViewBag.Departments = new SelectList(departmentService.GetAllDepartments().result, "Id", "Name");
        //    return View(model);
        //}

        // Edit old employee
        [HttpGet]
        public IActionResult Edit(string id)
        {
            if (string.IsNullOrEmpty(id))
                return NotFound("Employee ID not provided");

            var empResponse = employeeService.GetById(id);
            if (empResponse.IsHaveErrorOrNot || empResponse.result == null)
                return NotFound(empResponse.ErrorMessage);

            var departments = departmentService.GetAllDepartments();
            ViewBag.Departments = new SelectList(departments.result, "Id", "Name", empResponse.result.DepartmentId);

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
        [ActionName("Edit")] // Keep the URL /Employee/Edit/{id} for POST
        [ValidateAntiForgeryToken]
        public IActionResult EditPost(string id, EditEmployeeVM model)
        {
            if (!ModelState.IsValid)
            {
                var departments = departmentService.GetAllDepartments();
                ViewBag.Departments = new SelectList(departments.result, "Id", "Name", model.DepartmentId);
                return View(model);
            }

            var result = employeeService.EditEmployee(id, model);
            if (result.IsHaveErrorOrNot)
            {
                ModelState.AddModelError("", result.ErrorMessage);
                var departments = departmentService.GetAllDepartments();
                ViewBag.Departments = new SelectList(departments.result, "Id", "Name", model.DepartmentId);
                return View(model);
            }

            return RedirectToAction("Index");
        }


        //  Delete old employee
        [HttpGet]
        public IActionResult Delete(string id)
        {
            var empResponse = employeeService.GetById(id);
            if (empResponse.IsHaveErrorOrNot || empResponse.result == null)
                return NotFound(empResponse.ErrorMessage);

            return View(empResponse.result);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(string id)
        {
            var result = employeeService.DeleteEmployee(id);
            if (!result.IsHaveErrorOrNot)
                return RedirectToAction("Index");

            ViewBag.Error = result.ErrorMessage;
            return View("Delete", employeeService.GetById(id).result);
        }

    }
}
 