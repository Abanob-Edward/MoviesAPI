using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MoviesAPI.Models
{
    public class APPlicationDBContext : DbContext
    {
        public APPlicationDBContext(DbContextOptions<APPlicationDBContext> options ) : base(options)
        {

        }
        public DbSet<Genra> Genras { get; set; }
        public DbSet<Movie> Movies { get; set; }

    }
}
