using System;

namespace Store.Core.ErrorHandling.Exceptions
{
    /// <summary>
    /// Исключение, которое возникает при ошибке аутентификации
    /// </summary>
    public class AuthException : Exception
    {
        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="AuthException"/> с сообщением об ошибке
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        public AuthException(string message) : base(message)
        {

        }

        /// <summary>
        /// Создаёт новый экземпляр класса <seealso cref="AuthException"/> с сообщением об ошибке 
        /// и внутренним исключением
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public AuthException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
