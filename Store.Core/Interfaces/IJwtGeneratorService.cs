using Store.Data.Entities;

namespace Store.Core.Interfaces
{
    /// <summary>
    /// Интерфейс для генерации токенов
    /// </summary>
    public interface IJwtGeneratorService
    {
        /// <summary>
        /// Сгенерировать jwt-токен для пользователя
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>jwt-токен</returns>
        public string GenerateForUser(User user);
    }
}
