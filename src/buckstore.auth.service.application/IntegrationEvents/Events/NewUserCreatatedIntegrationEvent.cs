using System;
using buckstore.auth.service.application.Commands;
using buckstore.auth.service.domain.Events;

namespace buckstore.auth.service.application.IntegrationEvents.Events
{
    // change to use IntegrationEvent
    public class UserCreatedIntegrationEvent : Event
    {
        public Guid UserId { get; }
        public string Name { get; set; }
        public string Email { get; }
        public string Password { get; }
        public byte[] PasswordSalt { get; }
        public string Cpf { get; }

        public UserCreatedIntegrationEvent(CreateUserDto userDto)
        {
            UserId = userDto.Id;
            Name = userDto.Name;
            Email = userDto.Email;
            Password = userDto.Password;
            PasswordSalt = userDto.PasswordSalt;
            Cpf = userDto.Cpf;
        }
    }
}