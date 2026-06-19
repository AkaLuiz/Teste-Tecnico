using Microsoft.EntityFrameworkCore;
using RegistrosService.Enums;

public class RegistroServiceTests
{
    private readonly RegistroService _service;
    private readonly RegistrosDbContext _context;

    public RegistroServiceTests()
    {
        var options =
            new DbContextOptionsBuilder<RegistrosDbContext>()
                .UseInMemoryDatabase(
                    Guid.NewGuid().ToString())
                .Options;

        _context = new RegistrosDbContext(options);
        _service = new RegistroService(_context);
    }

    private async Task<Registro> CriarRegistroTeste()
    {
        var registro = new Registro
        {
            Id = Guid.NewGuid(),
            Tipo = TipoRegistro.Notificacao,
            NomeApresentante = "Teste",
            CpfCnpj = "52998224725",
            DataEntrada = DateOnly.FromDateTime(DateTime.UtcNow),
            Status = StatusRegistro.Pendente
        };

        _context.Registros.Add(registro);
        await _context.SaveChangesAsync();

        return registro;
    }

    [Fact]
    public async Task DevePermitir_PendenteParaRegistrado()
    {
        var registro = await CriarRegistroTeste();

        var resultado =
            await _service.AlterarStatus(
                registro.Id,
                new AlterarStatusRequest
                {
                    Status = StatusRegistro.Registrado
                });

        Assert.NotNull(resultado);
        Assert.Equal(
            StatusRegistro.Registrado,
            resultado.Status);
    }

    [Fact]
    public async Task DevePermitir_PendenteParaDevolvido()
    {
        var registro = await CriarRegistroTeste();

        var resultado =
            await _service.AlterarStatus(
                registro.Id,
                new AlterarStatusRequest
                {
                    Status = StatusRegistro.Devolvido
                });

        Assert.Equal(
            StatusRegistro.Devolvido,
            resultado.Status);
    }

    [Fact]
    public async Task DevePermitir_DevolvidoParaPendente()
    {
        var registro = await CriarRegistroTeste();

        await _service.AlterarStatus(
            registro.Id,
            new AlterarStatusRequest
            {
                Status = StatusRegistro.Devolvido
            });

        var resultado =
            await _service.AlterarStatus(
                registro.Id,
                new AlterarStatusRequest
                {
                    Status = StatusRegistro.Pendente
                });

        Assert.Equal(
            StatusRegistro.Pendente,
            resultado.Status);
    }

    [Fact]
    public async Task NaoDevePermitir_RegistradoParaOutroStatus()
    {
        var registro = await CriarRegistroTeste();

        await _service.AlterarStatus(
            registro.Id,
            new AlterarStatusRequest
            {
                Status = StatusRegistro.Registrado
            });

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.AlterarStatus(
                registro.Id,
                new AlterarStatusRequest
                {
                    Status = StatusRegistro.Devolvido
                }));
    }

    [Fact]
    public async Task NaoDevePermitir_MesmoStatus()
    {
        var registro = await CriarRegistroTeste();

        await Assert.ThrowsAsync<InvalidOperationException>(
            () => _service.AlterarStatus(
                registro.Id,
                new AlterarStatusRequest
                {
                    Status = StatusRegistro.Pendente
                }));
    }

    public class CpfCnpjValidatorTests
    {
        [Fact]
        public void ValidarCpf_DeveRetornarTrue_QuandoCpfValido()
        {
            var resultado =
                CpfCnpjValidator.Validar(
                    "52998224725");

            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCpf_DeveRetornarFalse_QuandoCpfInvalido()
        {
            var resultado =
                CpfCnpjValidator.Validar(
                    "12345678900");

            Assert.False(resultado);
        }

        [Fact]
        public void ValidarCnpj_DeveRetornarTrue_QuandoCnpjValido()
        {
            var resultado =
                CpfCnpjValidator.Validar(
                    "11222333000181");

            Assert.True(resultado);
        }

        [Fact]
        public void ValidarCnpj_DeveRetornarFalse_QuandoCnpjInvalido()
        {
            var resultado =
                CpfCnpjValidator.Validar(
                    "11111111111111");

            Assert.False(resultado);
        }

        [Fact]
        public void Validar_DeveRetornarFalse_QuandoValorForNulo()
        {
            var resultado =
                CpfCnpjValidator.Validar(
                    null);

            Assert.False(resultado);
        }

        [Fact]
        public void Validar_DeveRetornarFalse_QuandoValorForVazio()
        {
            var resultado =
                CpfCnpjValidator.Validar(
                    "");

            Assert.False(resultado);
        }
    }
}