using AutoMapper;
using MoviesAPI.Dtos;
using MoviesAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Helpers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Movie, MoviDetailsDto>();

            CreateMap<MovieDto, Movie>()
                .ForMember(src => src.Poster, obj => obj.Ignore());

        }
    }
}
