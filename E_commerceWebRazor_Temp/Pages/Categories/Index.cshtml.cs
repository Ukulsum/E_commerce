using E_BookWebRazor_Temp.Data;
using E_BookWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_BookWebRazor_Temp.Pages.Categories
{
    public class IndexModel : PageModel
    {
        private readonly ECommerceDbContext _context;
        public List<Category> CategoryList { get; set; }
        public IndexModel(ECommerceDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            CategoryList = _context.Categories.ToList();
        }
    }
}
