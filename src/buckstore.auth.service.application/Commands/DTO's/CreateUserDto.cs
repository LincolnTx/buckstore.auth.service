
using System;

namespace buckstore.auth.service.application.Commands
{
    public class CreateUserDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }
        public string Password { get; set; }
        public byte[] PasswordSalt { get; set; }

        public CreateUserDto(Guid id, string name, string surname, string email, string cpf, string password, byte[] passwordSalt)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Cpf = cpf;
            Password = password;
            PasswordSalt = passwordSalt;
        }
    }
}