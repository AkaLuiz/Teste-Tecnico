public static class CpfCnpjValidator
{
        public static bool Validar(string? cpfCnpj)
    {
        if (string.IsNullOrEmpty(cpfCnpj))
            return false;

        // Remover caracteres não numéricos
        string numeros = new string(cpfCnpj.Where(char.IsDigit).ToArray());

        if (numeros.Length == 11)
        {
            return ValidarCpf(numeros);
        }
        else if (numeros.Length == 14)
        {
            return ValidarCnpj(numeros);
        }

        return false;
    }

    public static bool ValidarCpf(string cpf)
    {
        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11)
            return false;

        if (cpf.Distinct().Count() == 1)
            return false;

        int[] multiplicador1 =
            { 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        int[] multiplicador2 =
            { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf[..9];

        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += (tempCpf[i] - '0') * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();

        tempCpf += digito;

        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += (tempCpf[i] - '0') * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cpf.EndsWith(digito);
    }

    public static bool ValidarCnpj(string cnpj)
    {
        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpj.Length != 14)
            return false;

        if (cnpj.Distinct().Count() == 1)
            return false;

        int[] multiplicador1 =
            { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        int[] multiplicador2 =
            { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj[..12];

        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += (tempCnpj[i] - '0') * multiplicador1[i];

        int resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        string digito = resto.ToString();

        tempCnpj += digito;

        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += (tempCnpj[i] - '0') * multiplicador2[i];

        resto = soma % 11;
        resto = resto < 2 ? 0 : 11 - resto;

        digito += resto.ToString();

        return cnpj.EndsWith(digito);
    }
}