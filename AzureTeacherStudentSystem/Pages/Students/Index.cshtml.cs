using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;

namespace AzureTeacherStudentSystem.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly DataContext _context;

        public IndexModel(DataContext context)
        {
            _context = context;
        }

    
		[BindProperty(SupportsGet = true)]
		public string FirstName { get; set; }

		[BindProperty(SupportsGet = true)]
		public string LastName { get; set; }

		[BindProperty(SupportsGet = true)]
		public string GroupName { get; set; }

		[BindProperty(SupportsGet = true)]
		public string Email { get; set; }

		public IList<Student> Student { get; set; } = default!;

		public async Task OnGetAsync()
		{
			var studentQuery = _context.Students
				.Include(s => s.Group)
				.AsQueryable();

			studentQuery = studentQuery
				.Where(s => string.IsNullOrEmpty(FirstName) || s.FirstName.Contains(FirstName))
				.Where(s => string.IsNullOrEmpty(LastName) || s.LastName.Contains(LastName))
				.Where(s => string.IsNullOrEmpty(GroupName) || s.Group.Name.Contains(GroupName))
				.Where(s => string.IsNullOrEmpty(Email) || s.Email.Contains(Email));

			Student = await studentQuery.ToListAsync();
		}
	}
}
