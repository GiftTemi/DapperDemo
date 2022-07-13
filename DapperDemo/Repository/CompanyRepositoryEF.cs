using DapperDemo.Data;
using DapperDemo.Models;
using System.Collections.Generic;
using System.Linq;

namespace DapperDemo.Repository
{
    public class CompanyRepositoryEF : ICompanyRepository
    {
        private readonly ApplicationDbContext _dbcontext;

        public CompanyRepositoryEF(ApplicationDbContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

        public Company Add(Company company)
        {
            _dbcontext.Add(company);
            _dbcontext.SaveChanges();
            return company;
        }

        public Company Find(int id)
        {
            return _dbcontext.Companies.FirstOrDefault(c => c.CompanyId == id);
        }

        public List<Company> GetAll()
        {
            return _dbcontext.Companies.ToList();
        }

        public void Remove(int id)
        {
         Company company  = _dbcontext.Companies.FirstOrDefault(c => c.CompanyId == id);
            _dbcontext.Companies.Remove(company);
            _dbcontext.SaveChanges();
            return;
        }

        public Company Update(Company company)
        {
            _dbcontext.Update(company);
            _dbcontext.SaveChanges();
            return company;
        }
    }
}
