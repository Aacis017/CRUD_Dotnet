using CRUD.Data;
using CRUD.Models;
using CRUD.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUD.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly CURDDemoDbContext curdDemoDbContext;

        public EmployeeController(CURDDemoDbContext curdDemoDbContext)
        {
            this.curdDemoDbContext = curdDemoDbContext;
        }

        [HttpGet]
        public async Task< IActionResult >Index() //to show employee list from database into the list of view 
        {
          var employees =  await curdDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost] //Adding employee to database from view
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest) {  
            var Employee = new Employee()
            {
                Id = Guid.NewGuid(),
                Name = addEmployeeRequest.Name,
                Email = addEmployeeRequest.Email,
                Salary = addEmployeeRequest.Salary,
                DateOfBirth = addEmployeeRequest.DateOfBirth,
                Department = addEmployeeRequest.Department,
            };

            await curdDemoDbContext.Employees.AddAsync(Employee);
            await curdDemoDbContext.SaveChangesAsync();
            return RedirectToAction("Add");
        }

        
        [HttpGet]

        public async Task<IActionResult>Edit(Guid id)
        {
            var employees =  await curdDemoDbContext.Employees.FirstOrDefaultAsync(x=> x.Id == id);

            if (employees !=null) { 
            var editModel = new UpdateEmployeeViewModel()
            
            
            {
                Id = employees.Id,
                Name = employees.Name,
                Email = employees.Email,
                Salary = employees.Salary,
                DateOfBirth = employees.DateOfBirth,
                Department = employees.Department

            };
                return View(editModel);
            }

            return RedirectToAction("Index");
            
        }

        [HttpPost]
        public async   Task<IActionResult> Edit(UpdateEmployeeViewModel model)
        {
            var employees = await curdDemoDbContext.Employees.FindAsync(model.Id);
            if (employees != null) {
                employees.Name = model.Name;
                employees.Email = model.Email;
                employees.Salary = model.Salary;
                employees.DateOfBirth = model.DateOfBirth;
                employees.Department = model.Department;
                await curdDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employees = await curdDemoDbContext.Employees.FindAsync(model.Id);
            if (employees != null)
            {
                curdDemoDbContext.Employees.Remove(employees); 
                await curdDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");

        }
    }
}
