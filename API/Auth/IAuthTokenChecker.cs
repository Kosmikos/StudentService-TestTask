namespace StudentServiceAPI.Auth
{
    public interface IAuthTokenChecker
    {
        bool IsValidToken(string token);
    }
}
