using DLL.Models;
using DLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class FilmService
    {
        private readonly FilmRepository _filmRepository;

        public FilmService(FilmRepository repository)
        {
            this._filmRepository = repository;
        }
        public async Task<List<Film>> GetFilms()
        {
            return (await _filmRepository.GetAllAsync()).ToList();
            
        }
        public async Task AddFilm(Film film)
        {
            await _filmRepository.CreateAsync(film);
        }
    }
}
