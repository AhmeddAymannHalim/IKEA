using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    //Inheritance : DepartmentController is  a Controller
    //Composition : DepartmentController has a IDepartmentService 
    public class DepartmentController : Controller
    {
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

        [HttpGet]
        public IActionResult Index()
        { 
            var departments = _departmentService.GetAllDepartments();
            return View(departments);
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
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
                if (_environment.IsDevelopment())
                {
                       message = ex.Message;
                        return View(departmentDto);
                }
                else
                {
                    message = "Department is not Created";
                    return View(nameof(HomeController.Error), message);
                }
                    

            }

        }

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
    }
}
