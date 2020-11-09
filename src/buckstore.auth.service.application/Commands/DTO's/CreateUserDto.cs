
namespace buckstore.auth.service.application.Commands
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Cpf { get; set; }

        public CreateUserDto(string name, string surname, string email, string cpf)
        {
            Name = name;
            Surname = surname;
            Email = email;
            Cpf = cpf;
        }
    }
}