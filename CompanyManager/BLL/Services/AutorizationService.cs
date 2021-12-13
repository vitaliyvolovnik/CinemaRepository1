using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AutorizationService
    {
        public EmployeeRepository employeeRepository;
        public AutorizationService(EmployeeRepository Repository)
        {
            this.employeeRepository = Repository;
        }
        public async Task<Employee> AutorizationAsync(string email,string password)
        {
            
                return (await employeeRepository.FindBuConditionAsync(x => x.Mail == email && x.Password == password))?.First();
           
        }
        public async Task<bool> ChangePasswordAsync(string email,string password,string newPassowrd)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(newPassowrd)) return false;
            return await employeeRepository.ChangePasswordAsync(email, password, newPassowrd);
        }
        
    }
}
