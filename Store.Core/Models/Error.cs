using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store.Core.Models
{
    /// <summary>
    /// Ошибка приложения
    /// </summary>
    public class Error
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
