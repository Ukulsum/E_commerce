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
    public class CompanyReposity : Repository<Company>, ICompanyRepository
    {
        private ECommerceDbContext _db;
        public CompanyReposity(ECommerceDbContext db) : base(db)
        {
            _db = db;
        }

        //public void Save()
        //{
        //    _db.SaveChanges();
        //}

        public void Update(Company oCompany)
        {
            _db.Companies.Update(oCompany);
        }
    }
}
