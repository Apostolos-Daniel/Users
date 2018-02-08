using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        [DataType(DataType.Password)]
        [BindProperty]
        public string Password { get; set; }

        private readonly IUsersRepository _repository;
        public IndexModel(IUsersRepository usersRepository)
        {
            _repository = usersRepository;
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
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
