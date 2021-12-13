using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class SessionHallService
    {
        SessionRepository sessionRepository;
        HallRepository hallRepository;
        public SessionHallService(SessionRepository session, HallRepository hall)
        {
            sessionRepository = session;
            hallRepository = hall;
        }
        
        public async Task<Session> AddSsesionAsync(Session session)
        {
            try
            {
                await sessionRepository.CreateAsync(session);
                return (await sessionRepository.FindBuConditionAsync(x=>x.StartTime==session.StartTime&&
                x.Hall.HallNumber==session.Hall.HallNumber))?.First();
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<CinemaHall>> GetAllHallsAsync()
        {
            return (await hallRepository.GetAllAsync())?.ToList();
        }
        public async Task<List<Session>> GetAllSessionsAsync()
        {
            return (await sessionRepository.GetAllAsync())?.ToList();
        }
        public async Task<List<Session>> GetAllNotFinishedSessionsAsync()
        {
            return (await sessionRepository.FindBuConditionAsync(x=>x.EndTime>DateTime.Now))?.ToList();
        }
    }
}
