
namespace buckstore.auth.service.application.Commands
{
    public class CreateUserDto
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public CreateUserDto(string name, string surname, string email)
        {
            Name = name;
            Surname = surname;
            Email = email;
        }
    }
}