namespace LTDCWebservice.Authentication
{
    public interface IJWTAuthenticationManager
    {
        string Authenticate(string username, string password);
    }
}
