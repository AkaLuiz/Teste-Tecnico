using Microsoft.EntityFrameworkCore;
public class AuthenticationService : IAuthenticationService
{
    private readonly AuthDbContext _context;
    private readonly JwtService _jwtService;

    public AuthenticationService(AuthDbContext context, JwtService jwtService)
    {
        _context = context;
        _jwtService = jwtService;
    }

    public Task<Usuario?> BuscarPorId(Guid id)
    {
        return _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
    }

    public Task<List<Usuario>> ListarUsuarios()
    {
        return _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario> CriarUsuario(RegistrarUsuarioRequest request)
    {

        bool emailExiste = await _context.Usuarios.AnyAsync(u => u.Email == request.Email);

        if (emailExiste)
        {
            throw new InvalidOperationException(
                "E-mail já cadastrado.");
        }

        var usuario = new Usuario
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email,
            Senha = BCrypt.Net.BCrypt.HashPassword(request.Senha),
            UsuarioPapel = request.Papel,
            Ativo = true
        };
        _context.Usuarios.Add(usuario);
        await _context.SaveChangesAsync();
        return usuario;
    }

    public async Task<LoginResponse> Login(LoginRequest request)
    {
        var usuario = await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == request.Email);
        
        if(usuario == null)
            throw new UnauthorizedAccessException("Credenciais inválidas.");
            //Solicitado que não seja possível identificar se o email ou senha estão incorretos.
        
        bool senhaValida = BCrypt.Net.BCrypt.Verify(request.Senha, usuario.Senha);
        if(!senhaValida)
            throw new UnauthorizedAccessException("Credenciais inválidas.");
            //Solicitado que não seja possível identificar se o email ou senha estão incorretos.

        if(!usuario.Ativo)
            throw new UnauthorizedAccessException("Usuário inativo.");
            //Como solicitado, usuário inativo não loga.

        string token = _jwtService.GerarToken(usuario);
        return new LoginResponse { Token = token };
    }
}