using System;
using System.Security.Cryptography;
using buckstore.auth.service.domain.SeedWork;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace buckstore.auth.service.domain.Aggregates.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string CredCard { get; private set; }
        public string  Cpf { get; private set; }
        public Address Address { get; private set; }
        public byte[] PasswordSalt { get; private set; }
        
        protected User() { }

        public User(string name, string surname, string email, string password, string credCard, string cpf, Address address)
        {
            Name = name;
            Surname = surname;
            Email = email;
            PasswordSalt = GenerateSalt();
            Password = CreateHashPassword(password, PasswordSalt);
            CredCard = credCard;
            Cpf = cpf;
            Address = address;
        }
        
        // deve ser alterado
        public bool LogInUser(string email, string password)
        {
            return true;
        }

        private  string CreateHashPassword(string password, byte [] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA1,
                iterationCount: 10000,
                numBytesRequested: 256 /8));
        }

        private byte[] GenerateSalt()
        {
            var salt = new byte[128 / 8];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(salt);

            return salt;
        }
    }
}