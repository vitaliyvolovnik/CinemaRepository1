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
        private readonly EmployeeRepository _employeeRepository;
        private readonly HallRepository _hallRepository;
        private readonly SeatRepository _seatRepository;
        private readonly SessionRepository _sessionRepository;

        public AdministrationService(EmployeeRepository Repository,
            HallRepository hallRepository,
            SessionRepository sessionRepository,
            SeatRepository seatRepository)
        {
            this._employeeRepository = Repository;
            this._hallRepository = hallRepository;
            this._sessionRepository = sessionRepository;
            this._seatRepository = seatRepository;
        }
        public async Task<Employee> AddWorkerAsync(Employee RegEmployee)
        {
            if (RegEmployee == null) return null;
            var employee = await _employeeRepository.FindBuConditionAsync(x => x.Mail == RegEmployee.Mail);
            if (employee.Count() == 1) return null;
            await _employeeRepository.CreateAsync(RegEmployee);
            return (await _employeeRepository.FindBuConditionAsync(x => x.Mail == RegEmployee.Mail))?.First();

        }
        public async Task<CinemaHall> AddHallAsync(CinemaHall hall)
        {
            var halls = (await _hallRepository.FindBuConditionAsync(x => x.HallNumber == hall.HallNumber)).ToList();
            if (halls.Count > 0) return null;
            try
            {
                await _hallRepository.CreateAsync(hall);
                return (await _hallRepository.FindBuConditionAsync(x => x.HallNumber == hall.HallNumber))?.First();
            }
            catch
            {
                return null;
            }
        }
        public async Task AddSeatAsync(Seat seat)
        {

            await _seatRepository.CreateAsync(seat);
        }
        public async Task<float> GetProfit(Session session)
        {
            var sum = 0f;
            var sess = (await _sessionRepository.FindBuConditionAsync(x => x.Id == session.Id)).First();
            foreach (var item in sess.Bookings)
            {
                if ((!item.IsCansel) && item.IsPaid)
                {
                    if (item.Seat.Type == "Premium") sum += sess.PremiumTiketPrice;
                    else sum += sess.TiketPrice;
                }
            }
            return sum;
        }
        
        
    }
}
