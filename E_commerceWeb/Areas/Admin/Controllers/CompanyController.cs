using E_Book.Models;
using E_Book.Models.ViewModels;
using E_Book.Utility;
using E_Book_DataAccess.Data;
using E_Book_DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Drawing;

namespace E_BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(SD.Role_Admin)]
    public class CompanyController : Controller
    {
        //private readonly ICompanyRepository _CompanyRepo;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
         {
            List<Company> oCompanyList = _unitOfWork.CompanyRepository.GetAll().ToList();
            return View(oCompanyList);
        }

        public IActionResult Upsert(int? id)
        {
            if(id == null || id == 0)
            {
                //Create
                return View(new Company());
            }
            else
            {
                //update
               
                return View(new Company());
            }
            
        }

        [HttpPost]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if(company.Id == 0)
                {
                    _unitOfWork.CompanyRepository.Add(company);
                }
                else
                {
                    _unitOfWork.CompanyRepository.Update(company);
                }
                
                _unitOfWork.Save();
                TempData["success"] = "Company Created successful";
                return RedirectToAction("Index");
            }
            else
            {
                return View(company);
            }
            return View(company);
        }

        
        public IActionResult Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            //Company? catId = _db.Categories.Find(id);
            Company? catId = _unitOfWork.CompanyRepository.Get(c => c.Id == id);
            if (catId == null)
            {
                return NotFound();
            }
            return View(catId);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id)
        {
            //Company? ocat = _db.Categories.Find(id);
            Company? ocat = _unitOfWork.CompanyRepository.Get(u => u.Id == id);
            if (ocat == null)
            {
                return NotFound();
            }
            //_db.Categories.Remove(ocat);
            //_db.SaveChanges();
            _unitOfWork.CompanyRepository.Remove(ocat);
            _unitOfWork.Save();
            TempData["success"] = "Company deleted successful";
            return RedirectToAction("Index");
        }

        #region API CALLs
        public IActionResult GetAll()
        {
            List<Company> oCompanyList = _unitOfWork.CompanyRepository.GetAll(includeProperties: "Category").ToList();
            return Json(new {data =  oCompanyList});
        }
        #endregion
    }
}
