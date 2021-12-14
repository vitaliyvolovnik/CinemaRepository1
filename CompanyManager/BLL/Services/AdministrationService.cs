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
        EmployeeRepository employeeRepository;
        HallRepository hallRepository;
        SeatRepository seatRepository;
        SessionRepository sessionRepository;
        public AdministrationService(EmployeeRepository Repository, HallRepository hallRepository, SessionRepository sessionRepository, SeatRepository seatRepository)
        {
            this.employeeRepository = Repository;
            this.hallRepository = hallRepository;
            this.sessionRepository = sessionRepository;
            this.seatRepository = seatRepository;
        }
        public async Task<Employee> AddWorkerAsync(Employee RegEmployee)
        {
            if (RegEmployee == null) return null;
            var employee = await employeeRepository.FindBuConditionAsync(x => x.Mail == RegEmployee.Mail);
            if (employee.Count() == 1) return null;
            await employeeRepository.CreateAsync(RegEmployee);
            return (await employeeRepository.FindBuConditionAsync(x => x.Mail == RegEmployee.Mail))?.First();

        }
        public async Task<CinemaHall> AddHallAsync(CinemaHall hall)
        {
            var halls = (await hallRepository.FindBuConditionAsync(x => x.HallNumber == hall.HallNumber)).ToList();
            if (halls.Count > 0) return null;
            try
            {
                await hallRepository.CreateAsync(hall);
                return (await hallRepository.FindBuConditionAsync(x => x.HallNumber == hall.HallNumber))?.First();
            }
            catch
            {
                return null;
            }
        }
        public async void AddSeatAsync(Seat seat)
        {
           await seatRepository.CreateAsync(seat);
        }
        public async Task<float> GetProfit(Session session)
        {
            var sum = 0f;
            var sess = (await sessionRepository.FindBuConditionAsync(x => x.Id== session.Id)).First();
            foreach (var item in sess.Bookings)
            {
                if ((!item.IsCansel)&&item.IsPaid) {
                    if (item.Seat.Type == "Premium") sum += sess.PremiumTiketPrice; 
                    else sum += sess.TiketPrice;
                }
            }
            return sum;
        }
        
    }
}
