using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.BLL.Services.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class EmployeeController : Controller
    {
        #region Services
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;
        

        public EmployeeController(
              IEmployeeService employeeService
            , ILogger<EmployeeController> logger,//ASK CLR TO CREATE OBJ FROM INTERFACE IEmployeeService
              IWebHostEnvironment environment
              )


        {
            _logger = logger;
            _environment = environment;
            _employeeService = employeeService;

        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index(string search)
        {
            var Employees = _employeeService.GetEmployees(search);

            if (!string.IsNullOrEmpty(search))
                return PartialView("./Partials/_EmployeeListPartial", Employees);

            return View(Employees);
           }

        #endregion

        #region Create
        [HttpGet]
        public IActionResult Create()
        {            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatedEmployeeDto employeeDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            var message = string.Empty;
            try
             {
                var result = _employeeService.CreateEmployee(employeeDto);

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Employee is Not Created !!!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(employeeDto);
                }

            }
            catch (Exception ex)
            {
                //01- Log Exception
                _logger.LogError(ex, ex.Message);

                //02- Set a Message
                message = _environment.IsDevelopment() ? ex.Message : "Error Can't Create the Employee";


            }

            ModelState.AddModelError(string.Empty, message);

            return View(employeeDto);

        }

        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var Employee = _employeeService.GetEmployeeById(id.Value);

            if (Employee is null)
                return NotFound();

            return View(Employee);

        }

        #endregion

        #region Edit
       [HttpGet]
        public IActionResult Edit(int? id)
       {
           if (id is null) return BadRequest();

           var employee = _employeeService.GetEmployeeById(id.Value);

           if (employee is null)
               return NotFound();

           return View(new UpdatedEmployeeDto()
           {
               Name = employee.Name,
               CreationDate = employee.CreationDate,
               EmployeeType = employee.EmployeeType,
               Salary =employee.Salary,
               Gender = employee.Gender,
               Address = employee.Address,
               Email = employee.Email,
               HiringDate = employee.HiringDate,
               IsActive = employee.IsActive,
               PhoneNumber = employee.PhoneNumber,
               Age = employee.Age,
              
               
           });

       }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id,UpdatedEmployeeDto employee)
        {
            if (!ModelState.IsValid) // ServerSide-Validation
                return View(employee);

            var message = string.Empty;

            try
            {
              
                var updated = _employeeService.UpdateEmployee(employee) > 0;

                if (updated)
                    return RedirectToAction(nameof(Index));

                message = "Error During Updating An Employee Object!";
            }
            catch (Exception ex)
            {
                //Log
                _logger.LogError(ex, ex.Message);


                //SetMessage

                message = _environment.IsDevelopment() ? ex.Message : "Error During Updating An Employee Object!";
            }
            ModelState.AddModelError(string.Empty, message);

            return View(employee);
        }


        #endregion

        #region Delete
     
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _employeeService.DeleteEmployee(id);

                if (deleted)
                    return RedirectToAction(nameof(Index));

                message = "an error has occured during deleting the Employee";
            }
            catch (Exception ex)
            {
                //Log Exception
                _logger.LogError(ex, ex.Message);

                //SetMessage
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during deleting the Employee";
            }

            //ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));




        }
        #endregion
      
    }
}
