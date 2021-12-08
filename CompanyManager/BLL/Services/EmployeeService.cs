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
        public async Task<Employee> AddWorkerAsync(Employee RegEmployee)
        {
            var employee = await employeeRepository.FindBuConditionAsync(x => x.Mail == RegEmployee.Mail);
            if (employee != null) return null;
            await employeeRepository.CreateAsync(RegEmployee);
            return RegEmployee;

        }
        public async Task<List<Employee>> GetAllAsync()
        {
            return (List<Employee>)await employeeRepository.GetAllAsync();
        }
        public async Task<List<Employee>> GetAllWhoWorkAsync()
        {
            return (List<Employee>)await employeeRepository.FindBuConditionAsync(x=>x.isFire);
        }
        public async Task<bool> Retire()
    }
}
