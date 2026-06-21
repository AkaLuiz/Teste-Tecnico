using RegistrosService.Enums;
using System.ComponentModel.DataAnnotations;

public class CadastrarRegistroRequest
{
    public TipoRegistro Tipo { get; set; }
    [Required(ErrorMessage = "Nome apresentante deve ser preenchido.")]
    public string? NomeApresentante { get; set; }
    [Required]
    [StringLength(18)]
    public string? CpfCnpj { get; set; }
    [Required]
    public DateOnly DataEntrada { get; set; }
    public string? Observacoes { get; set; }
}