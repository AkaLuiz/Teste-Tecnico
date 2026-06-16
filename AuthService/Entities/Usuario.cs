using AuthService.Enums;

public class Usuario
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Email { get; set; }
    public string? Senha { get; set; }
    public UsuarioPapel UsuarioPapel { get; set; }
    public bool Ativo { get; set; }

}