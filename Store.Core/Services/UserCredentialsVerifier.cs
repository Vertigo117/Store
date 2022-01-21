using Store.Core.Features.Queries;
using Store.Core.Interfaces;
using Store.Data.Entities;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Store.Core.Services
{
    /// <summary>
    /// Класс для проверки данных пользователя на валидность
    /// </summary>
    public class UserCredentialsVerifier : IUserCredentialsVerifier
    {
        ///<inheritdoc/>
        public bool VerifyRequestedCredentials(LoginQuery request, User user)
        {
            return BCryptNet.Verify(request.Password, user.Password) && user.Login == request.Login;
        }
    }
}
