﻿using LinkDev.IKEA.BLL.Models.Employees;
using LinkDev.IKEA.BLL.Services.Employees;
using LinkDev.IKEA.PL.ViewModels.Employees;
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
              IWebHostEnvironment environment)


        {
            _logger = logger;
            _environment = environment;
            _employeeService = employeeService;

        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var Employees = _employeeService.GetAllEmployees();
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
        //[HttpGet]
        //public IActionResult Edit(int? id)
        //{
        //    if (id is null) return BadRequest();

        //    var employee = _employeeService.GetEmployeeById(id.Value);

        //    if (employee is null)
        //        return NotFound();

        //    return View(new EmployeeEditViewModel()
        //    {
        //        Code = employee.Code,
        //        Name = employee.Name,
        //        Description = employee.Description,
        //        CreationDate = employee.CreationDate
        //    });

        //}

        //[HttpPost]
        //public IActionResult Edit(EmployeeEditViewModel employeeViewModel)
        //{
        //    if (!ModelState.IsValid) // ServerSide-Validation
        //        return View(employeeViewModel);

        //    var message = string.Empty;

        //    try
        //    {
        //        var employeeToUpdate = new UpdatedEmployeeDto()
        //        {
        //            Id = employeeViewModel.Id,
        //            Code = employeeViewModel.Code,
        //            Name = employeeViewModel.Name,
        //            Description = employeeViewModel.Description,
        //            CreationDate = employeeViewModel.CreationDate
        //        };

        //        var updated = _employeeService.UpdateEmployee(employeeToUpdate) > 0;

        //        if (updated)
        //            return RedirectToAction(nameof(Index));

        //        message = "Error During Updating An Employee Object!";
        //    }
        //    catch (Exception ex)
        //    {
        //        //Log
        //        _logger.LogError(ex, ex.Message);


        //        //SetMessage

        //        message = _environment.IsDevelopment() ? ex.Message : "Error During Updating An Employee Object!";


        //    }
        //    ModelState.AddModelError(string.Empty, message);

        //    return View(employeeViewModel);



        //}


        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();

            var Employee = _employeeService.GetEmployeeById(id.Value);

            if (Employee is null)
                return NotFound();

            return View(Employee);


        }
        #region Delete
        [HttpPost]
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
        #endregion
    }
}
