using MoviesAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
  public  interface IGenresService
    {
        Task<IEnumerable> GetAllGenres () ;
        Task<Genra> GetbyID(byte id);
        Task<Genra> AddGenre (Genra genra) ;
        Genra UpdateGenre(Genra genra) ;
        Genra  DeleteGenre (Genra genra) ;
        Task<bool> isvalidGenre(byte id);
    }
}
