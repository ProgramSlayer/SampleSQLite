using Microsoft.EntityFrameworkCore;
using WpfApp1.Models;

namespace WpfApp1.Context;

public class AkimLibDbContext : DbContext
{
	public DbSet<AkimLibItem> AkimLibItems { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=akimlib.db");
    }
}
