using E_BookWebRazor_Temp.Data;
using E_BookWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_BookWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class EditModel : PageModel
    {
        
        private readonly ECommerceDbContext _context;
        public Category cats { get; set; }

        public EditModel(ECommerceDbContext context)
        {
            _context = context;
        }
        public void OnGet(int? id)
        {
            if(id != null && id != 0)
            {
               cats = _context.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            if(ModelState.IsValid)
            {
                _context.Categories.Update(cats);
                _context.SaveChanges();
                TempData["success"] = "Category Updated successful";
                return RedirectToPage("Index");
            }
            return Page();
        }
    }
}
