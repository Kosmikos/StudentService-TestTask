using log4net;
using Microsoft.IdentityModel.Tokens;
using StudentServiceBL.Logging;
using System;
using System.IdentityModel.Tokens.Jwt;

namespace StudentServiceAPI.Auth
{
    internal class AuthTokenChecker : IAuthTokenChecker
    {
        AuthTokenCheckerOptions _options;
        ILog _logger;
        public AuthTokenChecker(AuthTokenCheckerOptions options, ILogFactory logFactory)
        {
            _options = options;
            _logger = logFactory.GetLogger(typeof(AuthTokenChecker));
        }
        public bool IsValidToken(string token)
        {

            var paramValidjwtToken = new TokenValidationParameters
            {
                IssuerSigningKey = _options.GetSymmetricSecurityKey(),
                ValidIssuer = _options.ISSUER,
                ValidateAudience = false,
                ValidateLifetime = false,
                ClockSkew = TimeSpan.FromSeconds(0) // Identity and resource servers are the same.
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                var resValidation = tokenHandler.ValidateToken(token, paramValidjwtToken, out SecurityToken secureToken);
                _logger.Debug($"token valid token: {token}");
                return true;
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.Warn($"token is expired token: {token}, error: {ex}");
                return false;
            }
            catch (SecurityTokenInvalidSignatureException ex)
            {
                _logger.Warn($"invalid signature token: {token}, error: {ex}");
                return false;
            }
            catch (SecurityTokenInvalidIssuerException ex)
            {
                _logger.Warn($"invalid issuer token: {token}, error: {ex}");
                return false;
            }
            catch (Exception ex)
            {
                _logger.Warn($"validate token fail token: {token}, error: {ex}");
                return false;
            }
        }
    }
}
