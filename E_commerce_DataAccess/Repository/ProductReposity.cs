using E_Book.Models;
using E_Book_DataAccess.Data;
using E_Book_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Book_DataAccess.Repository
{
    public class ProductReposity : Repository<Product>, IProductRepository
    {
        private ECommerceDbContext _db;
        public ProductReposity(ECommerceDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        public void Update(Product oproduct)
        {
            //_db.Products.Update(oproduct);

            var oproductfind = _db.Products.FirstOrDefault(u => u.Id == oproduct.Id);
            if(oproductfind != null)
            {
                oproductfind.Title = oproduct.Title;
                oproductfind.Description = oproduct.Description;
                oproductfind.Author = oproduct.Author;
                oproductfind.ListPrice = oproduct.ListPrice;
                oproductfind.ISBN = oproduct.ISBN;
                oproductfind.Price = oproduct.Price;
                oproductfind.Price50 = oproduct.Price50;
                oproductfind.Price100 = oproduct.Price100;
                oproductfind.CategoryId = oproduct.CategoryId;
                if(oproductfind.ImageUrl != null)
                {
                    oproductfind.ImageUrl = oproduct.ImageUrl;
                }
            }
        }
    }
}
