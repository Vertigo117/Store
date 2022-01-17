﻿using Store.Data.Entities;
using System;

namespace Store.Core.Models
{
    /// <summary>
    /// Результат авторизации
    /// </summary>
    public class AuthenticateResponse
    {
        /// <summary>
        /// Уникальный идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Логин
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Jwt-токен
        /// </summary>
        public string Token { get; set; }
    }
}
