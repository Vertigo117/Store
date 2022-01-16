using System;

namespace Store.Core.ErrorHandling.Exceptions
{
    /// <summary>
    /// Кастомная ошибка приложения
    /// </summary>
    public class CustomCoreException : Exception
    {
        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="CustomCoreException"/> с сообщением об ошибке
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public CustomCoreException(string message) : base(message)
        {

        }

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="CustomCoreException"/> с сообщением об ошибке
        /// и внутренним исключением
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public CustomCoreException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
