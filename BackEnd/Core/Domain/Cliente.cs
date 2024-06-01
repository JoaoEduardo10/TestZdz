using System.Net.Mail;
using System.Text.RegularExpressions;
using Core.Exceptions;
using Core.Exceptions.enums;

namespace Core.Domain
{
    public class Cliente
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }

        public Cliente(Guid id, string name, string email, string telefone)
        {
            Id = id;
            Name = MinNameLength(name);
            Email = IsValidEmail(email);
            Telefone = telefone;
        }

        public Cliente(string name, string email, string telefone)
        {
            Name = MinNameLength(name);
            Email = IsValidEmail(email);
            Telefone = telefone;
        }

        public Cliente()
        {
        }

        private static string MinNameLength(string name)
        {
            if (name.Length < 3)
            {
                throw new ProdutoException(ErrorCodeEnum.PRO001.GetMessage(), ErrorCodeEnum.PRO001.GetCode());
            }

            return name;
        }

        private static string IsValidEmail(string email)
        {

            try
            {
                var emailAddress = new MailAddress(email);

                return email;
            }
            catch
            {
                throw new ClienteException(ErrorCodeEnum.C0001.GetMessage(), ErrorCodeEnum.C0001.GetCode());
            }

        }

    }
}