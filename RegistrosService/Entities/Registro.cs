using RegistrosService.Enums;

public class Registro
{
    public Guid Id { get; set; }
    public TipoRegistro Tipo { get; set; }
    public string? NomeApresentante { get; set; }
    public string? CpfCnpj { get; set; }
    public DateOnly DataEntrada { get; set; } 
    public StatusRegistro Status { get; set; } = StatusRegistro.Pendente;
    public string? Observacoes { get; set; }
    public Guid? CriadoPor { get; set; }
    public DateTime CriadoEm { get; set; } = DateTime.UtcNow;
    public DateTime AtualizadoEm { get; set; } = DateTime.UtcNow;
}