using System.Collections.Generic;
using LibraryDemo.Models;

namespace LibraryDemo.Services
{
    public interface ICategoryService
    {
        IList<Category> GetAll();
    }
}