namespace GoPress.Mvc.Models.Responses
{
    public class AuthResponseViewModel
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public string AccessToken { get; set; }

        public string RefreshToken { get; set; }

        public string Role { get; set; }

    }
}
