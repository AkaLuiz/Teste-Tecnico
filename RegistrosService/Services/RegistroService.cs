using Microsoft.EntityFrameworkCore;
using RegistrosService.Enums;
public class RegistroService : IRegistroService
{

    private readonly RegistrosDbContext _context;

    public RegistroService(RegistrosDbContext context)
    {
        _context = context;
    }

    public Task<Registro?> BuscarPorId(Guid id)
    {
        return _context.Registros.FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<Registro>> ListarRegistros(RegistroFiltroRequest filtro)
    {
        IQueryable<Registro> query = _context.Registros;

        if (filtro.Tipo.HasValue)
        {
            query = query.Where(r => r.Tipo == filtro.Tipo.Value);
        }

        if (filtro.Status.HasValue)
        {
            query = query.Where(r => r.Status == filtro.Status.Value);
        }

        var page = Math.Max(1, filtro.Page);
        var limit = Math.Max(1, filtro.Limit);

        query = query.Skip((page - 1) * filtro.Limit).Take(limit);

        return await query.ToListAsync();
    }

    public async Task<List<Registro>> ListarTodosRegistros()
    {
        IQueryable<Registro> query = _context.Registros;
        return await query.ToListAsync();
    }

    public async Task<Registro> CriarRegistro(CadastrarRegistroRequest request, Guid usuarioId, string? usuarioNome)
    {

        bool cpfCnpjValido = CpfCnpjValidator.Validar(request.CpfCnpj);
        if (!cpfCnpjValido)
            throw new InvalidOperationException("CPF/CNPJ inválido.");

        bool dataEntradaValida = request.DataEntrada <= DateOnly.FromDateTime(DateTime.UtcNow);
        if (!dataEntradaValida)
            throw new InvalidOperationException("Data de entrada não pode ser futura.");

        if (request.DataEntrada == DateOnly.MinValue)
            throw new InvalidOperationException("Data de entrada é obrigatória.");

        bool nomeApresentanteInvalido = string.IsNullOrWhiteSpace(request.NomeApresentante);
        if (nomeApresentanteInvalido)
            throw new InvalidOperationException("Nome apresentante deve ser preenchido.");

        var registro = new Registro
        {
            Id = Guid.NewGuid(),
            Tipo = request.Tipo,
            NomeApresentante = request.NomeApresentante,
            CpfCnpj = request.CpfCnpj,
            DataEntrada = request.DataEntrada,
            Observacoes = request.Observacoes,
            CriadoPor = usuarioId,
            CriadoPorNome = usuarioNome
        };
        _context.Registros.Add(registro);
        await _context.SaveChangesAsync();
        return registro;
    }

    public async Task<Registro?> AtualizarRegistro(Guid id, AtualizarRegistroRequest request)
    {
        bool cpfCnpjValido = CpfCnpjValidator.Validar(request.CpfCnpj);
        if (!cpfCnpjValido)
            throw new InvalidOperationException("CPF/CNPJ inválido.");

        bool dataEntradaValida = request.DataEntrada <= DateOnly.FromDateTime(DateTime.UtcNow);
        if (!dataEntradaValida)
            throw new InvalidOperationException("Data de entrada não pode ser futura.");

        if (request.DataEntrada == DateOnly.MinValue)
            throw new InvalidOperationException("Data de entrada é obrigatória.");

        bool nomeApresentanteInvalido = string.IsNullOrWhiteSpace(request.NomeApresentante);
        if (nomeApresentanteInvalido)
            throw new InvalidOperationException("Nome apresentante deve ser preenchido.");

        var registro = await BuscarPorId(id);

        if (registro == null)
            return null;

        registro.Tipo = request.Tipo;
        registro.NomeApresentante = request.NomeApresentante;
        registro.CpfCnpj = request.CpfCnpj;
        registro.DataEntrada = request.DataEntrada;
        registro.Observacoes = request.Observacoes;

        registro.AtualizadoEm = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return registro;
    }

    public async Task<Registro?> AlterarStatus(Guid id, AlterarStatusRequest novoStatus)
    {
        var registro = await BuscarPorId(id);
        if (registro == null)
            return null;

        if (registro.Status == StatusRegistro.Registrado)
        {
            throw new InvalidOperationException("Registro já finalizado.");
        }

        if (registro.Status == novoStatus.Status)
        {
            throw new InvalidOperationException(
                "O registro já está neste status.");
        }

        registro.Status = novoStatus.Status;
        registro.AtualizadoEm = DateTime.UtcNow;
        await _context.SaveChangesAsync();
        return registro;
    }

    public async Task<Registro?> DeletarPorId(Guid id)
    {
        var registro = await BuscarPorId(id);
        if (registro == null)
            return null;
        _context.Registros.Remove(registro);
        await _context.SaveChangesAsync();
        return registro;
    }
}