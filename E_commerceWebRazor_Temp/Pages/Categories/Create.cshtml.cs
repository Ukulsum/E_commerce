using E_BookWebRazor_Temp.Data;
using E_BookWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_BookWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class CreateModel : PageModel
    {
        private readonly ECommerceDbContext _context;
        public Category category { get; set; }
        public CreateModel(ECommerceDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            TempData["success"] = "Category Created successful";
            return RedirectToPage("index");
        }
    }
}
