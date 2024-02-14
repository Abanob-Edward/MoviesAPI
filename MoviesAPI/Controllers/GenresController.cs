using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dtos;
using MoviesAPI.Models;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenresService _genresService;

        public GenresController(IGenresService genresService)
        {
          
            _genresService = genresService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAysync()
        {
            var genres = await _genresService.GetAllGenres();

            return Ok(genres);
        }
       
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GenreDto dto)
        {
            var genre = new Genra { Name = dto.Name };
            await _genresService.AddGenre(genre);
            return Ok(genre);
        }
        [HttpPut("{id}")]
        // NotFound($"there is no genre with id : {id}");
        public async Task<IActionResult> UpdateAsync(byte id,[FromBody] GenreDto dto)
        {
            var genre = await _genresService.GetbyID(id);
            if (genre == null)
            {
                return NotFound($"there is no genre with id : {id}");
            }
            genre.Name = dto.Name;
            _genresService.UpdateGenre(genre);
            return Ok(genre);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(byte id)
        {
            var genre = await _genresService.GetbyID(id);
            if (genre == null)
            {
                return NotFound($"there is no genre with id : {id}");
            }
            _genresService.DeleteGenre(genre);
            return Ok(genre);
        }


    }
}
