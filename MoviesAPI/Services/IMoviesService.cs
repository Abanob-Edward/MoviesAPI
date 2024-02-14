using MoviesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public interface IMoviesService
    {
        Task<IEnumerable<Movie>> GetAll(byte GenreID = 0); 
/*        Task<IEnumerable<Movie>> GetByGenreID(byte GenreID);*/
        Task<Movie> GetByID(int id); 
        Movie Update(Movie movie);
        Task<Movie> Add(Movie movie);
        Movie delete(Movie movie);
    }
}
