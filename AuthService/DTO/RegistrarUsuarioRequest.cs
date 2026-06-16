using AuthService.Enums;

public class RegistrarUsuarioRequest
{
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public UsuarioPapel Papel { get; set; }
}