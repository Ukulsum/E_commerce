using E_Book.Models;
using E_Book_DataAccess.Data;
using E_Book_DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace E_BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        //private readonly ICategoryRepository _categoryRepo;
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Category> oCategoryList = _unitOfWork.CategoryRepository.GetAll().ToList();
            return View(oCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category oCat)
        {
            //if(oCat.Name == oCat.CategoryOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
            //}

            if (ModelState.IsValid)
            {
                //_db.Categories.Add(oCat);
                //_db.SaveChanges();
                _unitOfWork.CategoryRepository.Add(oCat);
                _unitOfWork.Save();
                TempData["success"] = "Category Created successful";
                return RedirectToAction("Index");
            }
            return View(oCat);
        }

        public IActionResult Edit(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category? catId = _db.Categories.Find(id);
            Category? catId = _unitOfWork.CategoryRepository.Get(c => c.Id == id);
            //Category? catId1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
            //Category? catId2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
            if (catId == null)
            {
                return NotFound();
            }
            return View(catId);
        }

        [HttpPost]
        public IActionResult Edit(Category oCat)
        {
            if (ModelState.IsValid)
            {
                //_db.Categories.Update(oCat);
                //_db.SaveChanges();
                _unitOfWork.CategoryRepository.Update(oCat);
                _unitOfWork.Save();
                TempData["success"] = "Category updated successful";
                return RedirectToAction("Index");
            }
            return View(oCat);
        }

        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Category? catId = _db.Categories.Find(id);
            Category? catId = _unitOfWork.CategoryRepository.Get(c => c.Id == id);
            if (catId == null)
            {
                return NotFound();
            }
            return View(catId);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            //Category? ocat = _db.Categories.Find(id);
            Category? ocat = _unitOfWork.CategoryRepository.Get(u => u.Id == id);
            if (ocat == null)
            {
                return NotFound();
            }
            //_db.Categories.Remove(ocat);
            //_db.SaveChanges();
            _unitOfWork.CategoryRepository.Remove(ocat);
            _unitOfWork.Save();
            TempData["success"] = "Category deleted successful";
            return RedirectToAction("Index");
        }
    }
}
