using Microsoft.EntityFrameworkCore;

public class RegistrosDbContext : DbContext
{
    public RegistrosDbContext(DbContextOptions<RegistrosDbContext> options) : base(options)
    {
        
    }

    public DbSet<Registro> Registros => Set<Registro>();
}