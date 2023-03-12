namespace Asp.NetMVCCrud.Models
{
    public class LoginVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool KeepLoggedIn { get; set; }
    }
}
