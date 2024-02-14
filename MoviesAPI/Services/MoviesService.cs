using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dtos;
using MoviesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class MoviesService : IMoviesService
    {
        private readonly APPlicationDBContext _context;

        public MoviesService(APPlicationDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Movie>> GetAll(byte GenreID = 0)
        {
            var movies = await _context.Movies.Where(g=>g.GenreId ==GenreID || GenreID == 0)
                .OrderByDescending(x => x.Rate).Include(G => G.Genre).ToListAsync();
            return movies;
        }
       
        public async Task<Movie> GetByID(int id)
        {
           return await _context.Movies.Include(m =>m.Genre).SingleOrDefaultAsync(m => m.ID == id);
        }
        public Movie Update(Movie movie)
        {
            _context.Update(movie);
            _context.SaveChanges();
            return movie;
        }
        public async Task<Movie> Add(Movie movie)
        {
             await _context.Movies.AddAsync(movie);
            _context.SaveChanges();
            return movie;
        }

        public Movie delete(Movie movie)
        {
            _context.Remove(movie);
            _context.SaveChanges();
            return movie;
        }
    }
}
