using RegistrosService.Enums;

public class RegistroFiltroRequest
{
    public TipoRegistro? Tipo { get; set; }

    public StatusRegistro? Status { get; set; }

    public int Page { get; set; } = 1;

    public int Limit { get; set; } = 10;
}