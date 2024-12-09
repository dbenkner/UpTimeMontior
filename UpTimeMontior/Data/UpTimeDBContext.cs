using Microsoft.EntityFrameworkCore;

namespace UpTimeMontior.Data
{
    public class UpTimeDBContext : DbContext
    {
        public UpTimeDBContext( DbContextOptions<UpTimeDBContext> options ) : base( options ) { }   

        public DbSet<UpTimeMontior.Models.User> Users { get; set; } 
    }
}
