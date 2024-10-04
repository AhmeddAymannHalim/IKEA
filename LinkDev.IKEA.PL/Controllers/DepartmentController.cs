using AutoMapper;
using LinkDev.IKEA.BLL.Models.Departments;
using LinkDev.IKEA.BLL.Services.Departments;
using LinkDev.IKEA.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace LinkDev.IKEA.PL.Controllers
{
    //Inheritance : DepartmentController is  a Controller
    //Composition : DepartmentController has a IDepartmentService 
    [Authorize]
    public class DepartmentController : Controller
    {
        #region Services
        private readonly IDepartmentService _departmentService;
        private readonly IMapper _mapper;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(
              IDepartmentService departmentService,
              IMapper mapper,
              ILogger<DepartmentController> logger,//ASK CLR TO CREATE OBJ FROM INTERFACE IDepartmentService
              IWebHostEnvironment environment)


        {
            _logger = logger;
            _environment = environment;
            _departmentService = departmentService;
            _mapper = mapper;
        }
        #endregion

        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var departments = await _departmentService.GetAllDepartmentsAsync();
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
        public async Task<ActionResult> Create(DepartmentViewModel departmentVW)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            var message = string.Empty;
            try
            {
                //Manual Mapping
                //var createDepartment = new CreatedDepartmentDto()
                //{
                    
                //    Code = departmentVW.Code,
                //    Name = departmentVW.Name,
                //    Description = departmentVW.Description,
                //    CreationDate = departmentVW.CreationDate
                //};

                var departmentCreate =  _mapper.Map<CreatedDepartmentDto>(departmentVW);

                var created = await _departmentService.CreateDepartmentAsync(departmentCreate) > 0;

                //TempData : is a Property of Type Dictionary Object 
                //         : Transfering Data Between Two Consuctive Requests


                if (!created)
                
                {
                    message = "Department is Not Created";

                    ModelState.AddModelError(string.Empty, message);
                    return View(departmentVW);

                }
              

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                //01- Log Exception
                _logger.LogError(ex, ex.Message);

                //02- Set a Message
                message = _environment.IsDevelopment() ? ex.Message : "Error Can't Create the Department";
                TempData["Message"] = message;
                RedirectToAction(nameof(Index));

            }

            return Redirect(nameof(Index)); 
           

          

        }

        #endregion

        #region Details

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(department);

        }

        #endregion

        #region Edit
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();

            var departmentViewModel = _mapper.Map<DepartmentDetailsDto,DepartmentViewModel>(department);

            return View(departmentViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(DepartmentViewModel departmentViewModel)
        {
            if (!ModelState.IsValid) // ServerSide-Validation
                return View(departmentViewModel);

            var message = string.Empty;

            try
            {
                var departmentUpdate = _mapper.Map<DepartmentViewModel, UpdatedDepartmentDto>(departmentViewModel);

                var updated = await _departmentService.UpdateDepartmentAsync(departmentUpdate) > 0;

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
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var department = await _departmentService.GetDepartmentByIdAsync(id.Value);

            if (department is null)
                return NotFound();

            return View(department);


        }
        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = string.Empty;

            try
            {
                var deleted = await _departmentService.DeleteDepartmentAsync(id);

                if (deleted)
                    return  RedirectToAction(nameof(Index));


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
