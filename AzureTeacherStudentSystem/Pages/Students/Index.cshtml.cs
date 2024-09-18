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

        public IList<Student> Student { get;set; } = default!;

        public async Task OnGetAsync()
        {
            /*написати запит на отримання перших 20 студентів що вчаться у групі
                groupName і чиє імя містить букву а*/

            var groupName = "";

            Student = await _context.Students
                .Include(s => s.Group)
                .Where(s => s.Group.Name.Equals(groupName) && s.FirstName.Contains("a"))
                .Take(20)
                .ToListAsync();
        }
    }
}
