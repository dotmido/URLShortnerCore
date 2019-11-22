using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using URLShortner.Models;

namespace URLShortner
{
    public class CoreContext : DbContext
    {
        public CoreContext(DbContextOptions<CoreContext> options)
            : base(options)
        {
        }
        public DbSet<ShortURL> ShortenedURLs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(@"Server=tcp:dotmidoserver.database.windows.net,1433;Database=[ShortnerDB812E32DB];User ID=dotmido@dotmidoserver;Password=P@ssW0rd;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
            }
        }
    }
}
