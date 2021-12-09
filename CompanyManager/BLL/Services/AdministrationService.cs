using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class AdministrationService
    {
        public EmployeeRepository employeeRepository;
        HallRepository hallRepository;
        public AdministrationService(EmployeeRepository Repository,HallRepository hallRepository)
        {
            this.employeeRepository = Repository;
            this.hallRepository = hallRepository;
        }
        public async Task<Employee> AddWorkerAsync(Employee RegEmployee)
        {
            if (RegEmployee == null) return null;
            var employee = await employeeRepository.FindBuConditionAsync(x => x.Mail == RegEmployee.Mail);
            if (employee.Count() == 1) return null;
            await employeeRepository.CreateAsync(RegEmployee);
            return ((List<Employee>)await employeeRepository.FindBuConditionAsync(x => x.Mail == RegEmployee.Mail)).First();

        }
        public async Task<CinemaHall> AddHallAsync(CinemaHall hall)
        {
            var halls = (List<CinemaHall>)await hallRepository.FindBuConditionAsync(x => x.HallNumber == hall.HallNumber);
            if (halls.Count > 0) return null;
            try
            {
                await hallRepository.CreateAsync(hall);
                return ((List<CinemaHall>)await hallRepository.FindBuConditionAsync(x => x.HallNumber == hall.HallNumber)).First();
            }
            catch
            {
                return null;
            }
        }
    }
}
