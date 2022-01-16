namespace Store.Core.Models
{
    /// <summary>
    /// Настройки приложения
    /// </summary>
    public class AuthSettings
    {
        /// <summary>
        /// Секретный ключ
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Количество дней, в течение которого jwt-токен будет валиден
        /// </summary>
        public int LifeTimeDays { get; set; }
    }
}
