using StudentServiceBL.Logging;

namespace StudentServiceAPI.Auth
{
    internal class AuthTokenCheckerAlwaysTrue : IAuthTokenChecker
    {
        public bool IsValidToken(string token)
        {
            return true;
        }
    }
}
