using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Users.Backend;

namespace Users.Web.Pages
{
    public class IndexModel : PageModel
    {
        [TempData]
        public string StatusMessage { get; set; }

        public bool UserSubmitted { get; set; }
        
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        [BindProperty]
        public string EmailAddress { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$")]
        [StringLength(100, ErrorMessage = "The password must be at least 6 characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        [BindProperty]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        [BindProperty]
        public string ConfirmPassword { get; set; }

        private readonly IUsersRepository _repository;
        public IndexModel(IUsersRepository usersRepository)
        {
            _repository = usersRepository;
        }

        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid || ModelState.ValidationState == ModelValidationState.Invalid)
            {
                return Page();
            }
            UserSubmitted = true;

            User user = new User
            {
                EmailAddress = EmailAddress,
                Password = Password
            };
            UserValidator userValidator = new UserValidator(@"^(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,}$", 6);
            StatusMessage = _repository.AddUser(user, userValidator);
            
            return Page();
        }

        public IActionResult OnPostOk()
        {
            StatusMessage = string.Empty;
            return RedirectToPage("/Index");
        }
    }
}
