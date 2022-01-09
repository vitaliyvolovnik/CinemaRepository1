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
    public class SessionViewModel : BaseViewModel
    {

        private readonly FilmService _filmService;
        private readonly SessionHallService _sessionHallService;
        
        public SessionViewModel(FilmService fService, SessionHallService shService)
        {
            this._filmService = fService;
            this._sessionHallService = shService;
            Load();
            
        }

        //Add Session
        private Session addSession;
        public Session AddSession
        {
            get { if (addSession == null) addSession = new(); return addSession; }
            set { addSession = value; base.OnPropertyChanged("AddSession"); }
        }
        private ICommand addSessionComand;
        public ICommand AddSessionCommand
        {
            get { if (addSessionComand == null) addSessionComand = new RelayCommand(AddSessionExecute, AddSessionCanExecute); return addSessionComand; }
        }

        private bool AddSessionCanExecute(object obj)
        {
            return true;
        }

        private async void AddSessionExecute(object obj)
        {
            
            var session = await _sessionHallService.AddSsesionAsync(AddSession);
            if (session == null) return;
            AddSession = new();

        }
        //Films
        private ObservableCollection<Film> films;
        public ObservableCollection<Film> Films
        {
            get { if (films == null) films = new(); return films; }
        }
        //Hals
        private ObservableCollection<CinemaHall> halls;
        public ObservableCollection<CinemaHall> Halls
        {
            get
            { if (halls == null) halls = new(); return halls; }

        }
        public async void Load()
        {
            var films = await _filmService.GetFilms();
            foreach (var item in films)
                Films.Add(item);
            var list = await _sessionHallService.GetAllHallsWhithoutSeatsAsync();
            foreach (var item in list)
                Halls.Add(item);
        }
        
    }
}
