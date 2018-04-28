namespace Skillx.Gateways.WebAPI.Models.User
{
    public class RegisterUserRequest
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
