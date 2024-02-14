using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class MoviDetailsDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string GenreName { get; set; }
        public int year { get; set; }
        public double Rate { get; set; }

        public string Storeline { get; set; }
        public byte[] Poster { get; set; }

        public byte GenreId { get; set; }
    }
}
