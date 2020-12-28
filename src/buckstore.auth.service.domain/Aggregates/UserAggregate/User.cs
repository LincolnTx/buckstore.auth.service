using System;
using System.Security.Cryptography;
using buckstore.auth.service.domain.SeedWork;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace buckstore.auth.service.domain.Aggregates.UserAggregate
{
    public class User : Entity, IAggregateRoot
    {
        private string _name;
        public string Name => _name;
        private string _surname;
        public string Surname => _surname;
        private string _email;
        public string Email => _email;
        private string _password;
        private readonly string _credCard;
        private string _cpf;
        public string Cpf => _cpf;
        private int _userType;
        public int UserType => _userType;
        private byte[] _passwordSalt;
        public Address Address { get; set; }

        protected User() { }

        public User(string name, string surname, string email, string password, int userType)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _passwordSalt = GenerateSalt();
            _password = CreateHashPassword(password, _passwordSalt);
            _userType = userType;
        }
        public User(string name, string surname, string email, int userType)
        {
            _name = name;
            _surname = surname;
            _email = email;
            _password = string.Empty;
            _userType = userType;
        }

        public void AddUserCpf(string cpf)
        {
            _cpf = cpf;
        }

        public bool VerifyUserPassword(string password)
        {
            var requestPasswordSalted = CreateHashPassword(password, _passwordSalt);

            return string.Equals(requestPasswordSalted, _password);
        }

        public bool AddAddressForUserById(Address address)
        {
            return true;
        }

        public bool AddCredCardForUserById(string credCardNumber)
        {
            return true;
        }

        public void ChangePassword(string newPassword)
        {
            var saltForNewPassword = GenerateSalt();

            _password = CreateHashPassword(newPassword, saltForNewPassword);
            _passwordSalt = saltForNewPassword;
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