using System.ComponentModel.DataAnnotations;

namespace LinkDev.IKEA.PL.ViewModels.Departments
{
    public class DepartmentEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Code is Required Ya Prince !!")]
        public string Name { get; set; } = null!;

        public string Code { get; set; } = null!;

        public string? Description { get; set; }

        [Display(Name ="Creation Date")]
        public DateOnly CreationDate { get; set; }
    }
}
