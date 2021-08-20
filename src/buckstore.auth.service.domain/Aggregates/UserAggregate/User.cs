using System;
using System.Security.Cryptography;
using buckstore.auth.service.domain.Exceptions;
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
        private string _credCard;
        public string CredCard => _credCard;
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
            if (string.IsNullOrEmpty(_cpf))
            {
                _cpf = cpf;
            }
            else
            {
                throw new DomainException("Não é possível alterar o CPF");
            }
        }

        public bool UserInformationSet()
        {
            return !string.IsNullOrEmpty(_cpf);
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

        public void AddCredCardForUser(string credCardNumber)
        {
            if (string.IsNullOrEmpty(_credCard))
            {
                _credCard = credCardNumber;
            }

            if (_credCard != credCardNumber)
            {
                throw new DomainException("Parar alterar o cartão cadastrado, use as opções em minha conta");
            }
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
