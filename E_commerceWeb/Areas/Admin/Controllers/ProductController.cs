using E_Book.Models;
using E_Book.Models.ViewModels;
using E_Book_DataAccess.Data;
using E_Book_DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace E_BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        //private readonly IProductRepository _ProductRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
         {
            List<Product> oProductList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category").ToList();
            return View(oProductList);
        }

        public IActionResult Upsert(int? id)
        {
            //IEnumerable<SelectListItem> oCategoryList = _unitOfWork.CategoryRepository.GetAll().Select(u => new SelectListItem
            //{
            //    Text = u.Name,
            //    Value = u.Id.ToString()
            //});
            ////ViewBag.CategoryList = oCategoryList;
            //ViewData["CategoryList"] = oCategoryList;

            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                }),
                Product = new Product()
            };
            if(id == null || id == 0)
            {
                //Create
                return View(productVM);
            }
            else
            {
                //update
                productVM.Product = _unitOfWork.ProductRepository.Get(u => u.Id == id);
                return View(productVM);
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM oCat, IFormFile? file)
        {
            //if(oCat.Name == oCat.ProductOrder.ToString())
            //{
            //    ModelState.AddModelError("name", "The DisplayOrder cannot exactly match the name");
            //}

            if (ModelState.IsValid)
            {
               string wwwRootPath = _webHostEnvironment.WebRootPath;
                if(file != null) 
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); //FileName 
                    string productPath = Path.Combine(wwwRootPath, @"images/product"); //location where we have the save file

                    if (!string.IsNullOrEmpty(oCat.Product.ImageUrl))
                    {
                        //delete the old image
                        var oldImagePath = Path.Combine(wwwRootPath, oCat.Product.ImageUrl.TrimStart('\\'));
                        
                        if(System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    oCat.Product.ImageUrl = @"\images\product\" + fileName;
                }

                if(oCat.Product.Id == 0)
                {
                    _unitOfWork.ProductRepository.Add(oCat.Product);
                }
                else
                {
                    _unitOfWork.ProductRepository.Update(oCat.Product);
                }
                
                _unitOfWork.Save();
                TempData["success"] = "Product Created successful";
                return RedirectToAction("Index");
            }
            else
            {

                oCat.CategoryList = _unitOfWork.CategoryRepository.GetAll().Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });
                return View(oCat);
            }
            return View(oCat);
        }

        //public IActionResult Edit(int id)
        //{
        //    if (id == null || id == 0)
        //    {
        //        return NotFound();
        //    }
        //    //Product? catId = _db.Categories.Find(id);
        //    Product? catId = _unitOfWork.ProductRepository.Get(c => c.Id == id);
        //    //Product? catId1 = _db.Categories.FirstOrDefault(u=>u.Id == id);
        //    //Product? catId2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
        //    if (catId == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(catId);
        //}

        //[HttpPost]
        //public IActionResult Edit(Product oCat)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        //_db.Categories.Update(oCat);
        //        //_db.SaveChanges();
        //        _unitOfWork.ProductRepository.Update(oCat);
        //        _unitOfWork.Save();
        //        TempData["success"] = "Product updated successful";
        //        return RedirectToAction("Index");
        //    }
        //    return View(oCat);
        //}

        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Product? catId = _db.Categories.Find(id);
            Product? catId = _unitOfWork.ProductRepository.Get(c => c.Id == id);
            if (catId == null)
            {
                return NotFound();
            }
            return View(catId);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            //Product? ocat = _db.Categories.Find(id);
            Product? ocat = _unitOfWork.ProductRepository.Get(u => u.Id == id);
            if (ocat == null)
            {
                return NotFound();
            }
            //_db.Categories.Remove(ocat);
            //_db.SaveChanges();
            _unitOfWork.ProductRepository.Remove(ocat);
            _unitOfWork.Save();
            TempData["success"] = "Product deleted successful";
            return RedirectToAction("Index");
        }

        #region API CALLs
        public IActionResult GetAll()
        {
            List<Product> oProductList = _unitOfWork.ProductRepository.GetAll(includeProperties: "Category").ToList();
            return Json(new {data =  oProductList});
        }
        #endregion
    }
}
