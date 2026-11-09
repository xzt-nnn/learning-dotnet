using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RazorPagesMovie.Data;
using RazorPagesMovie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RazorPagesMovie.Pages.MoviePages
{
    public class IndexModel : PageModel
    {
        private readonly RazorPagesMovieContext _context;

        public IndexModel(RazorPagesMovieContext context)
        {
            _context = context;
        }

        public IList<Movie> Movie { get; set; } = default!;

        [BindProperty(SupportsGet =true)]
        public string? SearchString {  get; set; }

        public SelectList? Generes { get; set; }

        [BindProperty(SupportsGet =true)]
        public string? MovieGenre {  get; set; }
        public async Task OnGetAsync()
        {
            // 下拉框数据源：所有去重后的类型
            IQueryable<string> genreQuery = from m in _context.Movie
                                            orderby m.Genre
                                            select m.Genre;

            var movies = from m in _context.Movie
                         select m;

            if (!string.IsNullOrEmpty(SearchString))
            {
                // SQLite 的 Contains 区分大小写，统一转小写实现忽略大小写搜索
                var keyword = SearchString.ToLower();
                movies = movies.Where(s => s.Title != null && s.Title.ToLower().Contains(keyword));
            }

            if (!string.IsNullOrEmpty(MovieGenre))
            {
                movies = movies.Where(x => x.Genre == MovieGenre);
            }

            Generes = new SelectList(await genreQuery.Distinct().ToListAsync());
            Movie = await movies.ToListAsync();
        }
    }
}