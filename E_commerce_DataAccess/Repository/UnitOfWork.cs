using E_Book.Models;
using E_Book_DataAccess.Data;
using E_Book_DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Book_DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ECommerceDbContext _context;
        public ICategoryRepository CategoryRepository { get; private set; }
        public IProductRepository ProductRepository { get; private set; }
        public ICompanyRepository CompanyRepository { get; private set; }
        public UnitOfWork(ECommerceDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryReposity(_context);
            ProductRepository = new ProductReposity(_context);
            CompanyRepository = new CompanyReposity(_context);
        }
        
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
