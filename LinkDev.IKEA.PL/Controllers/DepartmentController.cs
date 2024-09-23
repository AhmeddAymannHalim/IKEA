using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LinkDev.IKEA.PL.Controllers
{
    //Inheritance : DepartmentController is  a Controller
    //Composition : DepartmentController has a IDepartmentService 
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(
              IDepartmentService departmentService
            , ILogger<DepartmentController> logger,//ASK CLR TO CREATE OBJ FROM INTERFACE IDepartmentService
              IWebHostEnvironment environment)


        {
            _logger = logger;
            _environment = environment;
            _departmentService = departmentService;

        }
        #endregion

        #region Index
        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
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
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            var message = string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(departmentDto);

                if (result > 0)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    message = "Department is Not Created !!!";
                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentDto);
                }

            }
            catch (Exception ex)
            {
                //01- Log Exception
                _logger.LogError(ex, ex.Message);

                //02- Set a Message
                message = _environment.IsDevelopment() ? ex.Message : "Error Can't Create the Department";


            }

            ModelState.AddModelError(string.Empty, message);

            return View(departmentDto);

        }

        #endregion

        #region Details

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);

        }

        #endregion

        #region Edit
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(new DepartmentEditViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate
            });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(DepartmentEditViewModel departmentViewModel)
        {
            if (!ModelState.IsValid) // ServerSide-Validation
                return View(departmentViewModel);

            var message = string.Empty;

            try
            {
                var deprtmentToUpdate = new UpdatedDepartmentDto()
                {
                    Id = departmentViewModel.Id,
                    Code = departmentViewModel.Code,
                    Name = departmentViewModel.Name,
                    Description = departmentViewModel.Description,
                    CreationDate = departmentViewModel.CreationDate
                };

                var updated = _departmentService.UpdateDepartment(deprtmentToUpdate) > 0;

                if (updated)
                    return RedirectToAction(nameof(Index));

                message = "Error During Updating An Department Object!";
            }
            catch (Exception ex)
            {
                //Log
                _logger.LogError(ex, ex.Message);


                //SetMessage

                message = _environment.IsDevelopment() ? ex.Message : "Error During Updating An Department Object!";


            }
            ModelState.AddModelError(string.Empty, message);

            return View(departmentViewModel);



        }


        #endregion

        #region Delete
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id is null) return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);


        }
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = _departmentService.DeleteDepartment(id);

                if (deleted)
                    return RedirectToAction(nameof(Index));


                message = "an error has occured during deleting the department";
            }
            catch (Exception ex)
            {
                //Log Exception
                _logger.LogError(ex, ex.Message);

                //SetMessage
                message = _environment.IsDevelopment() ? ex.Message : "an error has occured during deleting the department";
            }

            //ModelState.AddModelError(string.Empty, message);
            return RedirectToAction(nameof(Index));




        }  
        #endregion
        #endregion
    }
}
