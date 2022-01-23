using Store.Core.Features.Queries;
using Store.Data.Entities;

namespace Store.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для верификации пользовательских данных
    /// </summary>
    public interface IUserCredentialsService
    {
        /// <summary>
        /// Проверить логин пользователя и пароль
        /// </summary>
        /// <param name="request">Запрос для верификации</param>
        /// <param name="user">Данные пользователя</param>
        /// <returns>Результат верификации</returns>
        bool VerifyRequestedCredentials(LoginQuery request, User user);
    }
}
