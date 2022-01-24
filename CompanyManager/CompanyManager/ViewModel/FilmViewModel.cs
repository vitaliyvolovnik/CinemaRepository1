using BLL.Services;
using CompanyManager.Core;
using DLL.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CompanyManager.ViewModel
{
    public class FilmViewModel : BaseViewModel
    {
        private readonly AdministrationService _administrationService;
        private readonly FilmService _filmService;

        public FilmViewModel(AdministrationService service, FilmService filmService)
        {
            this._administrationService = service;
            this._filmService = filmService;
            LoadFilm();
        }

        //add film
        private Film addFilm;
        public Film AddFilm
        {
            get { if (addFilm == null) addFilm = new(); return addFilm; }
            set { addFilm = value; base.OnPropertyChanged("AddFilm"); }
        }
        private ObservableCollection<Film> films;
        public ObservableCollection<Film> Films
        {
            get { if (films == null) films=new(); return films; }
        }
        public async void LoadFilm()
        {
            try
            {
                var films = await _filmService.GetFilms();
                foreach (var item in films)
                {
                    Films.Add(item);
                }
            }
            catch
            {
                Thread.Sleep(600);
                LoadFilm();
            }
        }
        //Add command
        private ICommand addFilmCommand;
        public ICommand AddFilmCommand
        {
            get { if (addFilmCommand == null) addFilmCommand = new RelayCommand(AddFilmExecute,AddFilmCanExecute);return addFilmCommand; }
        }
        private bool AddFilmCanExecute(object obj)
        {
            
            if (!Regex.IsMatch(addFilm.Genre, "^[a-z-]{3,10}$")) return false;
            if (addFilm.Price <= 0) return false;
            return !string.IsNullOrWhiteSpace(addFilm.Name);
        }
        private async void AddFilmExecute(object obj)
        {
            var date = AddFilm.PlayTime;
            AddFilm.PlayTime = new DateTime(1,1,1,date.Hour,date.Minute,date.Second);
            await _filmService.AddFilm(AddFilm);
            Films.Add(AddFilm);
            addFilm = new();
        }
    }
}
