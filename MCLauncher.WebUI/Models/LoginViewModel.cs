namespace MCLauncher.WebUI.Models
{
    public class LoginViewModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool UserAlreadyLogged { get; set; }
        public bool ForceLoginRequired { get; set; }
    }
}
