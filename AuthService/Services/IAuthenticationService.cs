public interface IAuthenticationService
{
    Task<LoginResponse> Login(LoginRequest request);

    Task<Usuario> CriarUsuario(RegistrarUsuarioRequest request);

    Task<Usuario?> BuscarPorId(Guid id);
}