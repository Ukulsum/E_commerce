using E_BookWebRazor_Temp.Data;
using E_BookWebRazor_Temp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace E_BookWebRazor_Temp.Pages.Categories
{
    [BindProperties]
    public class DeleteModel : PageModel
    {
        private readonly ECommerceDbContext _context;
        public Category pCats { get; set; }
        public DeleteModel(ECommerceDbContext context)
        {
            _context = context;
        }
        public void OnGet(int? id)
        {
            if(id !=null && id != 0)
            {
                pCats = _context.Categories.Find(id);
            }
        }

        public IActionResult OnPost()
        {
            Category? ocats = _context.Categories.Find(pCats.Id);
            if(ocats != null)
            {
                NotFound();
            }
            _context.Categories.Remove(ocats);
            _context.SaveChanges();
            TempData["success"] = "Category Deleted successful";
            return RedirectToPage("Index");
        }
    }
}
