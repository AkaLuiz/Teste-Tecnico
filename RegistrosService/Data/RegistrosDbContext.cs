using Microsoft.EntityFrameworkCore;
using RegistrosService.Enums;

public class RegistrosDbContext : DbContext
{
    public RegistrosDbContext(DbContextOptions<RegistrosDbContext> options) : base(options)
    {

    }

    public DbSet<Registro> Registros => Set<Registro>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Registro>().HasData(
            new Registro
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                Tipo = TipoRegistro.Contrato,
                NomeApresentante = "Empresa Contrato LTDA",
                CpfCnpj = "42591651000143",
                DataEntrada = new DateOnly(2026, 6, 20),
                Status = StatusRegistro.Pendente,
                Observacoes = "Registro seed contrato",
                CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                CriadoPorNome = "Admin",
                CriadoEm = new DateTime(2026, 6, 20, 10, 0, 0, DateTimeKind.Utc),
                AtualizadoEm = new DateTime(2026, 6, 20, 10, 0, 0, DateTimeKind.Utc)
            },

            new Registro
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                Tipo = TipoRegistro.Procuracao,
                NomeApresentante = "João da Silva",
                CpfCnpj = "42591651000143",
                DataEntrada = new DateOnly(2026, 6, 19),
                Status = StatusRegistro.Pendente,
                Observacoes = "Registro seed procuração",
                CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                CriadoPorNome = "Registrador",
                CriadoEm = new DateTime(2026, 6, 19, 9, 0, 0, DateTimeKind.Utc),
                AtualizadoEm = new DateTime(2026, 6, 19, 9, 0, 0, DateTimeKind.Utc)
            },

            new Registro
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000004"),
                Tipo = TipoRegistro.Notificacao,
                NomeApresentante = "Empresa Notificação SA",
                CpfCnpj = "42591651000143",
                DataEntrada = new DateOnly(2026, 6, 18),
                Status = StatusRegistro.Pendente,
                Observacoes = "Registro seed notificação",
                CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                CriadoPorNome = "Consulta",
                CriadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc),
                AtualizadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc)
            },
                        new Registro
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000005"),
                Tipo = TipoRegistro.Notificacao,
                NomeApresentante = "Empresa Notificação SA",
                CpfCnpj = "42591651000143",
                DataEntrada = new DateOnly(2026, 6, 18),
                Status = StatusRegistro.Pendente,
                Observacoes = "Registro seed notificação",
                CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                CriadoPorNome = "Consulta",
                CriadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc),
                AtualizadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc)
            },
                        new Registro
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000006"),
                Tipo = TipoRegistro.Notificacao,
                NomeApresentante = "Empresa Notificação SA",
                CpfCnpj = "42591651000143",
                DataEntrada = new DateOnly(2026, 6, 18),
                Status = StatusRegistro.Pendente,
                Observacoes = "Registro seed notificação",
                CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                CriadoPorNome = "Consulta",
                CriadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc),
                AtualizadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc)
            },
                        new Registro
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000007"),
                Tipo = TipoRegistro.Notificacao,
                NomeApresentante = "Empresa Notificação SA",
                CpfCnpj = "42591651000143",
                DataEntrada = new DateOnly(2026, 6, 18),
                Status = StatusRegistro.Pendente,
                Observacoes = "Registro seed notificação",
                CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                CriadoPorNome = "Consulta",
                CriadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc),
                AtualizadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc)
            },
                        new Registro
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000008"),
                Tipo = TipoRegistro.Notificacao,
                NomeApresentante = "Empresa Notificação SA",
                CpfCnpj = "42591651000143",
                DataEntrada = new DateOnly(2026, 6, 18),
                Status = StatusRegistro.Pendente,
                Observacoes = "Registro seed notificação",
                CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                CriadoPorNome = "Consulta",
                CriadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc),
                AtualizadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc)
            },
                        new Registro
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000009"),
                Tipo = TipoRegistro.Notificacao,
                NomeApresentante = "Empresa Notificação SA",
                CpfCnpj = "42591651000143",
                DataEntrada = new DateOnly(2026, 6, 18),
                Status = StatusRegistro.Pendente,
                Observacoes = "Registro seed notificação",
                CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                CriadoPorNome = "Consulta",
                CriadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc),
                AtualizadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc)
            },
                        new Registro
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000010"),
                Tipo = TipoRegistro.Notificacao,
                NomeApresentante = "Empresa Notificação SA",
                CpfCnpj = "42591651000143",
                DataEntrada = new DateOnly(2026, 6, 18),
                Status = StatusRegistro.Pendente,
                Observacoes = "Registro seed notificação",
                CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                CriadoPorNome = "Consulta",
                CriadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc),
                AtualizadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc)
            },
                        new Registro
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000011"),
                Tipo = TipoRegistro.Notificacao,
                NomeApresentante = "Empresa Notificação SA",
                CpfCnpj = "42591651000143",
                DataEntrada = new DateOnly(2026, 6, 18),
                Status = StatusRegistro.Pendente,
                Observacoes = "Registro seed notificação",
                CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                CriadoPorNome = "Consulta",
                CriadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc),
                AtualizadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc)
            },
                        new Registro
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000003"),
                Tipo = TipoRegistro.Notificacao,
                NomeApresentante = "Empresa Notificação SA",
                CpfCnpj = "42591651000143",
                DataEntrada = new DateOnly(2026, 6, 18),
                Status = StatusRegistro.Pendente,
                Observacoes = "Registro seed notificação",
                CriadoPor = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                CriadoPorNome = "Consulta",
                CriadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc),
                AtualizadoEm = new DateTime(2026, 6, 18, 8, 0, 0, DateTimeKind.Utc)
            }
        );
    }

}