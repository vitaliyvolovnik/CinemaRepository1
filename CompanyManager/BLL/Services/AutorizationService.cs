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
        public async Task<Employee> Autorization(string email,string password)
        {
            var employee = await employeeRepository.FindBuConditionAsync(x => x.Mail == email && x.Password == password);
            if (employee == null) return null;
            var emp = employee as List<Employee>;
            return emp[0];
        }
        
    }
}
