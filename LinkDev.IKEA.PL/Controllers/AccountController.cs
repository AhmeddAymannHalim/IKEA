using LinkDev.IKEA.DAL.Entities.Identity;
using LinkDev.IKEA.PL.ViewModels.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PL.Controllers
{
    public class AccountController : Controller
    {
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;

		public AccountController(
                                UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
			_userManager = userManager;
			_signInManager = signInManager;
		}

        #region SignUp

        [HttpGet] //Account/Signup
        public IActionResult Signup()
        {
            return View();
        }

		[HttpPost] //Account/Signup
		public async Task<IActionResult> Signup(SignUpViewModel model)
		{
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByNameAsync(model.UserName);

            if(user is { })
				ModelState.AddModelError(nameof(SignUpViewModel.UserName), "This UserName is Already UsedBy Another Account !!");


			if (user is null)


            {  
                
                user = new ApplicationUser()
				{

					FName = model.FirstName,
					LName = model.LastName,
					UserName = model.UserName,
					Email = model.Email,
					IsAgree = model.IsAgree

				};
				var result = await _userManager.CreateAsync(user, model.Password);

				if (result.Succeeded)
					return RedirectToAction(nameof(SignIn));

				foreach (var error in result.Errors)
					ModelState.AddModelError(string.Empty, error.Description);

			}


			
            

			return View(model);
		}


        #endregion

        #region SignIn
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if(user is { })
            {
                var flag = await _userManager.CheckPasswordAsync(user, model.Password);
                if(flag)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, true);

                    if (result.IsNotAllowed)
                        //If The Account Not Confirmed .. it will not be SignIn
                        ModelState.AddModelError(string.Empty, "Your Account is not Confirmed yet...");

                    if(result.IsLockedOut)
                        ModelState.AddModelError(string.Empty, "Your Account is Closed ...");


                    if (result.Succeeded)
						return RedirectToAction(nameof(HomeController.Index),"Home");



				}
			}

            ModelState.AddModelError(string.Empty, "Invalid Login Attempt");


            return View(model);
        }
        #endregion
    }
}
