namespace Store.Api.Contracts
{
    /// <summary>
    /// Ошибка приложения
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// Сообщение для пользователя
        /// </summary>
        public string UserMessage { get; set; }

        /// <summary>
        /// Подробное сообщение об ошибке
        /// </summary>
        public string ExceptionMessage { get; set; }

        /// <summary>
        /// Стек вызовов
        /// </summary>
        public string StackTrace { get; set; }
    }
}
