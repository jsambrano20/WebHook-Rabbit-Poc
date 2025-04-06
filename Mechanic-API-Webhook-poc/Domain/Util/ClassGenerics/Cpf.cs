using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace User_API_Webhook_poc.Domain.Util.ClassGenerics
{
    [Owned]
    public class Cpf
    {
        public string Value { get; }

        protected Cpf() { } // usado pelo EF

        public Cpf(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("CPF não pode ser vazio.");

            value = Regex.Replace(value, "[^0-9]", ""); // remove pontuação

            if (!IsValidCpf(value))
                throw new ArgumentException("CPF inválido.");

            Value = value;
        }

        public override string ToString()
        {
            return Convert.ToUInt64(Value).ToString(@"000\.000\.000\-00");
        }

        public static bool IsValidCpf(string cpf)
        {
            if (cpf.Length != 11 || cpf.All(c => c == cpf[0]))
                return false;

            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string tempCpf = cpf.Substring(0, 9);
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            int resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            string digito = resto.ToString();
            tempCpf += digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;
            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito += resto.ToString();
            return cpf.EndsWith(digito);
        }

        public override bool Equals(object obj)
        {
            return obj is Cpf cpf && Value == cpf.Value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static implicit operator string(Cpf cpf) => cpf.Value;
        public static explicit operator Cpf(string value) => new Cpf(value);
    }
}
