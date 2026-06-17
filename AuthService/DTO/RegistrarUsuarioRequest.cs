using AuthService.Enums;
using System.ComponentModel.DataAnnotations;

public class RegistrarUsuarioRequest
{
    [Required]
    public string? Name { get; set; }
    [Required]
    [EmailAddress]
    public string? Email { get; set; }
    [Required]
    public string? Senha { get; set; }
    [Required]
    public UsuarioPapel Papel { get; set; }
}