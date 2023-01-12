using Contracts;
using Entities.Models;
using Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public sealed class CompanyRepository:RepositoryBase<Company> , ICompanyRepository
    {
        public CompanyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Company> GetAllCompanies()
        {
          return  FiindAll().OrderBy(c => c.Name).ToList();
        }

        public Company? GetCompanyById(Guid companyId)
        {
          return FindByCondition(c => c.Id.Equals(companyId)).SingleOrDefault();
        }

        public void CreateCompany(Company company)
        {
            Create(company);

        }

        public void DeleteCompany(Company company)
        {
            Delete(company);
        }

        public void UpdateCompany(Company company)
        {
            Update(company);
        }
    }
    }

