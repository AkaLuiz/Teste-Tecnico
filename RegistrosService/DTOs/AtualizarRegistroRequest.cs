using RegistrosService.Enums;

public class AtualizarRegistroRequest
{
    public TipoRegistro Tipo { get; set; }

    public string? NomeApresentante { get; set; }

    public string? CpfCnpj { get; set; }

    public DateOnly DataEntrada { get; set; }

    public string? Observacoes { get; set; }
}