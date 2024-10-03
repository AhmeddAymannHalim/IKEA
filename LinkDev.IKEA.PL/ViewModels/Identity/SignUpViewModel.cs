using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Identity
{
	public class SignUpViewModel
	{
		[Required(ErrorMessage ="UserName is required")]
        public string UserName { get; set; } = null!;

		[EmailAddress]
        public string Email { get; set; } = null!;


		[Display(Name ="First Name")]
		public string FirstName { get; set; } = null!;

		[Display(Name = "Last Name")]
		public string LastName { get; set; } = null!;

		
        [DataType(DataType.Password)]
		public string Password { get; set; } = null!;

		[Display(Name ="Confirm Password")]
		[DataType(DataType.Password)]
		[Compare("Password",ErrorMessage = "Password doesn't match with password")]
		public string ConfirmPassword { get; set; } = null!;

		//[Required] bool Required By Default
		public bool IsAgree { get; set; }
    }
}
