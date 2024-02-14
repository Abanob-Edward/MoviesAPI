using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Dtos;
using MoviesAPI.Models;
using MoviesAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase {

    
        private readonly IMoviesService _moviesService;
        private readonly IGenresService _genresService;
        private readonly IMapper _autoMapper;

        public MoviesController(IMoviesService moviesService, IGenresService genresService , IMapper mapper)
        {
            _moviesService = moviesService;
            _genresService = genresService;
            _autoMapper = mapper;
        }

        private new List<string> _allowedExtensions = new List<string> { ".png", ".jpg", ".jpeg" };
        private long _maxAllowedPosterSize = 1048576;
    


        // get
        //api/mivies
        [HttpGet]

        public async Task<IActionResult> GetAllAsync()
        {
            var movies = await _moviesService.GetAll();
            var data = _autoMapper.Map<IEnumerable<MoviDetailsDto>>(movies);
            // TODO : map movies to dto by autoMapper

            return Ok(data);
        }


        //api/movies/GetByGanreId?Genreid=1
        [HttpGet("GetByGanreId")]
        public async Task<IActionResult> GetByGanreIdAsync(byte Genreid)
        {
            var movies = await _moviesService.GetAll(Genreid);
            // TODO : map movies to dto 
            var data = _autoMapper.Map<IEnumerable<MoviDetailsDto>>(movies);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            // find by id and iclude 
            var movie = await _moviesService.GetByID(id);
            // check null 
            if (movie == null)
                return BadRequest("Invalid ID");
            var dto = _autoMapper.Map<MoviDetailsDto>(movie);

            return Ok(dto);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(int id, [FromForm] MovieDto dto)
        {
            var movie = await _moviesService.GetByID(id);
            if(movie == null)
                return BadRequest($"This Id is Invalid {id}");


            var isvalidGenre = await _genresService.isvalidGenre(dto.GenreId);
            if (!isvalidGenre)
                return BadRequest("Invalid Genra Id" + dto.GenreId);

            if(dto.Poster != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("The Allowed Extensions is .png and .jpg");
                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max Allowed size for Poster is 1 MB");
                using var datastream = new MemoryStream();

                await dto.Poster.CopyToAsync(datastream);
                movie.Poster = datastream.ToArray();
            }
            movie.Title         = dto.Title;
            movie.Rate          = dto.Rate;
            movie.Storeline     = dto.Storeline;
            movie.year          = dto.year;
            movie.GenreId       = dto.GenreId;
           _moviesService.Update(movie);
            return Ok(movie);
        }

        [HttpPost]
        [Route("UpLodeImage")]


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] MovieDto dto)
        {
            if (dto.GenreId == 0)
                return BadRequest("Invalid Genre Id");

            if(dto.Poster != null)
            {
                if (!_allowedExtensions.Contains(Path.GetExtension(dto.Poster.FileName).ToLower()))
                    return BadRequest("The Allowed Extensions is .png and .jpg");
                if (dto.Poster.Length > _maxAllowedPosterSize)
                    return BadRequest("Max Allowed size for Poster is 1 MB");
            }
           

            var isvalidGenre = await _genresService.isvalidGenre(dto.GenreId);
            if (!isvalidGenre)
                return BadRequest("Invalid Genra Id" + dto.GenreId);

            using var datastream = new MemoryStream();

            await dto.Poster.CopyToAsync(datastream);

            var movie = _autoMapper.Map<Movie>(dto);
            movie.Poster = datastream.ToArray();
            _moviesService.Add(movie);
            return Ok(movie);
        }
       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsnc(int id)
        {
            var movie = await _moviesService.GetByID(id);
            if (movie == null)
                return NotFound($"This Id is Invalid {id}");

            _moviesService.delete(movie);
            return Ok(movie );
        }
    }
}
