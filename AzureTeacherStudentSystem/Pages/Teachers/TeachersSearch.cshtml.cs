using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AzureTeacherStudentSystem.Pages.Teachers
{
    public class TeachersSearchModel : PageModel
    {
        [BindProperty(SupportsGet = true)]
        public string FirstName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string LastName { get; set; }

        [BindProperty(SupportsGet = true)]
        public int? MinSalary { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        public void OnGet()
        {
        }
    }
}
