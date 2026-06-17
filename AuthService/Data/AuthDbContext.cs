using AuthService.Enums;
using Microsoft.EntityFrameworkCore;

public class AuthDbContext : DbContext
{
    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
        
    }

    public DbSet<Usuario> Usuarios => Set<Usuario>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasIndex(u => u.Email)
                .IsUnique();

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Name = "Admin",
                    Email = "admin@example.com",
                    // Senha: "admin123"
                    Senha = "$2a$11$wsHii5Yy26cQv2vhAsZJmuu2Is/dtyU9OKpwmbxhp73BvYjhK26my",
                    UsuarioPapel = UsuarioPapel.Admin,
                    Ativo = true
                },
                new Usuario
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Name = "Registrador",
                    Email = "registrador@example.com",
                    // Senha: "registrador123"
                    Senha = "$2a$11$2W47yF7X7vkJ1L6WIxuLOudbt9va6JK79twHwt0T5bEeKsitCgyM",
                    UsuarioPapel = UsuarioPapel.Registrador,
                    Ativo = true
                },
                new Usuario
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Name = "Consulta",
                    Email = "consulta@example.com",
                    // Senha: "consulta123"
                    Senha = "$2a$11$Evh01g.08S7I/dteTfdLB.P.jAb.DgWU6cSMMCHbMCYHqE/06mruG",
                    UsuarioPapel = UsuarioPapel.Consulta,
                    Ativo = true
                }
            );
        }
    
}