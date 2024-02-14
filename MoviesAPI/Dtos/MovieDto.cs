using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Dtos
{
    public class MovieDto
    {
/*        [MaxLength(250)]
*/      public string Title { get; set; }
        public int year { get; set; }
        public double Rate { get; set; }
/*        [MaxLength(2500)]*/
        public string Storeline { get; set; }
        public IFormFile? Poster { get; set; }

        public byte GenreId { get; set; }
    }
}
