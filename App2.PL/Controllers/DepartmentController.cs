using App2.BLL.ModelVM.Department;
using App2.BLL.Service.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace App2.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            this.departmentService = departmentService;
        }
         
        //  List Departments
        
        public IActionResult Index()
        {
            var result = departmentService.GetAllDepartments();
            return View(result);
        }

   
        // Department Details
    
        public IActionResult Details(int id)
        {
            var result = departmentService.GetDepartmentById(id);
            if (result.IsHaveErrorOrNot || result.result == null)
                return NotFound(result.ErrorMessage);

            return View(result.result);
        }
         
        //  Create Department
 
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateDepartmentVM model)
        {
            if (ModelState.IsValid)
            {
                var result = departmentService.Create(model);
                if (!result.IsHaveErrorOrNot)
                    return RedirectToAction("Index");

                ViewBag.Error = result.ErrorMessage;
            }

            return View(model);
        }
         
        // Edit Department
     
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var response = departmentService.GetDepartmentById(id);
            if (response.IsHaveErrorOrNot || response.result == null)
                return NotFound(response.ErrorMessage);

            var model = new EditDepartmentVM
            {
                Id = response.result.Id,
                Name = response.result.Name,
                Area = response.result.Area
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(int id, EditDepartmentVM model)
        {
            if (ModelState.IsValid)
            {
                var result = departmentService.EditDepartment(id, model);
                if (!result.IsHaveErrorOrNot)
                    return RedirectToAction("Index");

                ViewBag.Error = result.ErrorMessage;
            }

            return View(model);
        }

    
        //  Delete Department
     
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var response = departmentService.GetDepartmentById(id);
            if (response.IsHaveErrorOrNot || response.result == null)
                return NotFound(response.ErrorMessage);

            return View(response.result);
        }

        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            var result = departmentService.DeleteDepartment(id);
            if (!result.IsHaveErrorOrNot)
                return RedirectToAction("Index");

            ViewBag.Error = result.ErrorMessage;
            return View("Delete", departmentService.GetDepartmentById(id).result);
        }
    }
}
