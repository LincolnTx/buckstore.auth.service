using System;
using System.Security.Cryptography;
using buckstore.auth.service.domain.SeedWork;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace buckstore.auth.service.domain.Aggregates.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        private string _name;
        public string GetName => _name;
        private string _surname;
        public string GetSurname => _surname;
        private string _email;
        public string GetEmail => _email;
        private string _password;
        public string GetPassword => _password;
        private string _credCard; //cred card might be a value obj
        private string  _cpf;
        public string GetCpf => _cpf;
        public Address Address { get; set; }
        private byte[] _passwordSalt;
        public byte[] GetSalt => _passwordSalt;
        protected User() { }

        public User(string name, string surname, string email, string password, string cpf)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _passwordSalt = GenerateSalt();
            _password = CreateHashPassword(password, _passwordSalt);
            _cpf = cpf;
        }
        
        // deve ser alterado
        public bool LogInUser(string email, string password)
        {
            return true;
        }

        public bool AddAddressForUserById(Address address)
        {
            return true;
        }

        public bool AddCredCardForUserById(string credCardNumber)
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