using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeService
    {
        public EmployeeRepository employeeRepository;
        public EmployeeService(EmployeeRepository Repository)
        {
            this.employeeRepository = Repository;
        }
        public async Task<List<Employee>> GetAllAsync()
        {
            return (await employeeRepository.GetAllAsync()).ToList();
        }
        public async Task<List<Employee>> GetAllWhoWorkAsync()
        {
            return (await employeeRepository.FindBuConditionAsync(x=>!x.isFire))?.ToList();
        }
        public async Task<List<Employee>> GetEmployeeAsync(string name,string surname)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            return (await employeeRepository.FindBuConditionAsync(x => x.Name == name&&x.Surname==surname))?.ToList();
        }
        public async Task<bool> FireAsync(Employee employee)
        {
            if (employee == null)
                return false;
            return await this.employeeRepository.FireAsync(employee);
        }
    }
}
