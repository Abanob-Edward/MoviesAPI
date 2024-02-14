using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [MaxLength(250)]
        public string  Title{ get; set; }
        public int  year{ get; set; }
        public double  Rate{ get; set; }
        [MaxLength(2500)]
        public string  Storeline{ get; set; }
        public byte[]  Poster{ get; set; }
        public byte GenreId { get; set; }
        public Genra Genre { get; set; }

    }
}
