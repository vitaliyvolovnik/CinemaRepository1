using BLL.Services;
using CompanyManager.Core;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManager.ViewModel
{
    public class HallsViewModel : BaseViewModel
    {

        private readonly AdministrationService _administrationService;
        private readonly SessionHallService _sessionHallService;
        private CinemaHall addHall;
        public string premiumRows;

        private ObservableCollection<CinemaHall> halls;
       /* public HallsViewModel(AdministrationService administration, SessionHallService sessionHall) : base()
        {
            _administrationService = administration;
            _sessionHallService = sessionHall;
        }*/
        public CinemaHall AddHall
        {
            get
            {
                if (addHall == null) AddHall = new CinemaHall();
                return addHall;
            }
            set
            {
                addHall = value;
                base.OnPropertyChanged("AddHall");
            }
        }
        public ObservableCollection<CinemaHall> Halls
        {
            get
            {
                if (Halls == null) Halls = new ();
                return Halls;
            }
            set
            {
                Halls = value;
                base.OnPropertyChanged("Halls");
            }
        }
        private ICommand addHallCommand;
        public ICommand AddHallCommand
        {
            get
            {
                if (addHallCommand == null) addHallCommand = new RelayCommand(AddHallExecute, AddHallCunExecute);
                return addHallCommand;
            }
        }

        private async void AddHallExecute(object obj)
        {
            var res = await _administrationService.AddHallAsync(AddHall);
            if (res == null) return;
            List<int> premiums = new();
            foreach(var item in premiumRows.Split(','))
            {
                var num = 0;
                int.TryParse(item, out num);
                premiums.Add(num);
            }
            Halls.Add(res);
            for(int i = 0;i< res.Rows;i++)
            {
                var type = (premiums.Where(x=>x==i).Count()>0) ? "Premium" : "default";
                for (int j = 0; j < res.SeatsInRow; j++)
                {
                    var seat = new Seat()
                    {
                        Row = i + 1,
                        SeatInRow = j + 1,
                        Hall = res,
                        Type = type
                        
                    };
                    _administrationService.AddSeatAsync(seat);
                }
            }    
            AddHall = new CinemaHall();
            premiumRows = "";
        }

        private bool AddHallCunExecute(object obj)
        {
            if (AddHall.HallNumber == 0) return false;
            if (addHall.Rows==0) return false;
            if (addHall.SeatsInRow==0) return false;
            if (string.IsNullOrWhiteSpace(addHall.ScreenDiagonal)) return false;


            return true;
        }
    }
}
