using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;

namespace RazorPagesMovie.Pages.MoviePages
{
    public class EditModel : PageModel
    {
        //构造函数注入依赖
        private readonly RazorPagesMovieContext _context;

        public EditModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        //绑定HTTP请求里的表单数据
        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _context.Movie.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            Movie = movie;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Entry(Movie).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
