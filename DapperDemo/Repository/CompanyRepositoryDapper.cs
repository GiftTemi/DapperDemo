using Dapper;
using DapperDemo.Data;
using DapperDemo.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DapperDemo.Repository
{
    public class CompanyRepositoryDapper : ICompanyRepository
    {
        private IDbConnection dbConnection;

        public CompanyRepositoryDapper(IConfiguration configuration)
        {
           this.dbConnection = new SqlConnection(configuration.GetConnectionString("DefaultConnection"));
        }
        public Company Add(Company company)
        {
            var sql = "INSERT INTO Companies (Name, Address, City, State, PostalCode) VALUES (@Name, @Address, @City, @State, @PostalCode);"
            + "SELECT CAST(SCOPE_IDENTITY() as int); ";
            var id =dbConnection.Query<int>(sql,company).Single();
            company.CompanyId = id;
            return company;
        }

        public Company Find(int id)
        {
            var sql = "SELECT * FROM Companies WHERE CompanyId = @CompanyId";
            return dbConnection.Query<Company>(sql, new { @CompanyId = id }).Single();
        }

        public List<Company> GetAll()
        {
            var sql = "SELECT * FROM Companies";
            return dbConnection.Query<Company>(sql).ToList();
        }

        public void Remove(int id)
        {
            var sql = "DELETE FROM Companies WHERE CompanyId = @Id";
            dbConnection.Execute(sql, new { id });
        }

        public Company Update(Company company)
        {
            var sql = "UPDATE Companies SET Name = @Name, Address = @Address , City = @City, " +
                "State = @State, PostalCode = @PostalCode WHERE CompanyId = @CompanyId";
            dbConnection.Execute(sql, company);
            return company;
        }
    }
}
