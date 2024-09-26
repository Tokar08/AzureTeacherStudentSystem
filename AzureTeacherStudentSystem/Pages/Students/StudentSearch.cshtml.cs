using AzureTeacherStudentSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AzureTeacherStudentSystem.Pages.Students
{
    public class StudentSearchModel : PageModel
    {
        private readonly Data.DataContext _context;

        public StudentSearchModel(Data.DataContext context)
        {
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string FirstName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string LastName { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SelectedGroup { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Email { get; set; }

        public List<Group> Groups { get; set; } = new();

        public async Task OnGetAsync()
        {
            Groups = await _context.Groups.ToListAsync();
        }
    }
}
