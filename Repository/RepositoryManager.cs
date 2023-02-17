using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        

        private readonly RepositoryContext _repositoryContext;
        //
        private readonly Lazy<ICompanyRepository> _companyRepository;
        private readonly Lazy<IEmployeeRepository> _employeeRepository;
        private readonly Lazy<IUserRepository> _userRepository;
        public RepositoryManager(RepositoryContext repositoryContext , IMapper mapper, UserManager<User> userManager)
        {
            _repositoryContext = repositoryContext;
            _companyRepository = new Lazy<ICompanyRepository>(() => new
            CompanyRepository(repositoryContext));

            _employeeRepository = new Lazy<IEmployeeRepository>(() => new
            EmployeeRepository(repositoryContext));

            _userRepository = new Lazy<IUserRepository>(() =>
            new UserRepository( userManager, mapper));
        }
        
        /// ta take object from repository user
        public ICompanyRepository Company => _companyRepository.Value;
        public IEmployeeRepository Employee => _employeeRepository.Value;

        public IUserRepository userRepository => _userRepository.Value;

        public void Save() => _repositoryContext.SaveChanges();
    }
}
