namespace Store.Core.Models
{
    /// <summary>
    /// Результат выполнения запроса пользовательских данных
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Зашифрованный пароль пользователя
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Роль пользователя в системе
        /// </summary>
        public string Role { get; set; }
    }
}
