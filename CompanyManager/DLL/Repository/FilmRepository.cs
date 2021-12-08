using DLL.Context;
using DLL.Models;
using DLL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLL.Repository
{
    public class FilmRepository:BaseRepository<Film>
    {
        public FilmRepository(CinemaContext context) : base(context) { }
    }
}
