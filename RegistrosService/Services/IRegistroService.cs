using RegistrosService.Enums;

public interface IRegistroService
{
    Task<Registro> CriarRegistro(CadastrarRegistroRequest request, Guid usuarioId);

    Task<List<Registro>> ListarRegistros(RegistroFiltroRequest filtro);

    Task<Registro?> BuscarPorId(Guid id);

    Task<Registro?> DeletarPorId(Guid id);

    Task<Registro?> AtualizarRegistro(Guid id, AtualizarRegistroRequest request);

    Task<Registro?> AlterarStatus(Guid id, AlterarStatusRequest novoStatus);

}