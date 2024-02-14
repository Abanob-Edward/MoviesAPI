using Microsoft.EntityFrameworkCore;
using MoviesAPI.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Services
{
    public class GenresServic : IGenresService
    {
        private readonly APPlicationDBContext  _context;
        //private readonly IGenresService _genresService;
        public GenresServic(APPlicationDBContext context/*, IGenresService genresService*/)
        {
            _context = context;
            //_genresService = genresService;
        }

        public async Task<Genra> AddGenre(Genra genre)
        {
         
            await _context.Genras.AddAsync(genre);
            _context.SaveChanges();
            return genre;
        }

        public Genra DeleteGenre(Genra genra)
        {
            _context.Remove(genra);
            _context.SaveChanges();
            return genra;
        }

        public async  Task<IEnumerable> GetAllGenres()
        {
            return  await _context.Genras.OrderBy(m => m.Name).ToListAsync();
        }

        public async Task<Genra> GetbyID(byte id)
        {
            return await _context.Genras.SingleOrDefaultAsync(g => g.ID == id);
        }

        public async Task<bool> isvalidGenre(byte id)
        {
            return await _context.Genras.AnyAsync(g => g.ID == id);
        }

            public Genra UpdateGenre(Genra genra)
        {
            _context.Update(genra);
            _context.SaveChanges();
            return genra;
         }
    }
}
