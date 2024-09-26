using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AzureTeacherStudentSystem.Data;
using AzureTeacherStudentSystem.Models;


namespace AzureTeacherStudentSystem.Pages.Teachers
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
		public int? Salary { get; set; }

		[BindProperty(SupportsGet = true)]
		public string Email { get; set; }

		public IList<Teacher> Teacher { get; set; } = default!;


		public async Task OnGetAsync()
		{
			var teacherQuery = _context.Teachers
				.Include(t => t.Subjects)
				.AsQueryable();

			teacherQuery = teacherQuery
				.Where(t => string.IsNullOrEmpty(FirstName) || t.FirstName.Contains(FirstName))
				.Where(t => string.IsNullOrEmpty(LastName) || t.LastName.Contains(LastName))
				.Where(t => !Salary.HasValue || t.Salary >= Salary)
				.Where(t => string.IsNullOrEmpty(Email) || t.Email.Contains(Email));

			Teacher = await teacherQuery.ToListAsync();
		}
	}
}
