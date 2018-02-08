using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
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
        
        [Required]
        [EmailAddress]
        [BindProperty]
        public string EmailAddress { get; set; }

        [Required]
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

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid || (ModelState.ValidationState == ModelValidationState.Invalid))
            {
                return Page();
            }

            User user = new User
            {
                EmailAddress = EmailAddress,
                Password = Password
            };
            StatusMessage = _repository.AddUser(user);

            return RedirectToPage("Index");
        }
    }
}
