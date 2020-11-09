namespace buckstore.auth.service.application.Commands
{
    public class LoginUserDto
    {
        public string Email { get; set; }
        public string Name { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }

        public LoginUserDto(string email, string name, string token, string refreshToken)
        {
            Email = email;
            Name = name;
            Token = token;
            RefreshToken = refreshToken;
        }
    }
}