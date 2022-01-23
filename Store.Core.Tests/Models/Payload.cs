namespace Store.Core.Tests.Models
{
    /// <summary>
    /// Данные токена
    /// </summary>
    internal class Payload
    {
        /// <summary>
        /// Логин
        /// </summary>
        public string Unique_name { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Not valid before
        /// </summary>
        public string Nbf { get; set; }

        /// <summary>
        /// Время истечения
        /// </summary>
        public string Exp { get; set; }

        /// <summary>
        /// Время выпуска
        /// </summary>
        public string Iat { get; set; }

        public override bool Equals(object obj)
        {
            var payload = obj as Payload;
            return payload.Nbf == Nbf
                && payload.Unique_name == Unique_name
                && payload.Role == Role
                && payload.Iat == Iat
                && payload.Exp == Exp;
        }

        public override int GetHashCode()
        {
            return Unique_name.GetHashCode() 
                + Role.GetHashCode() 
                + Nbf.GetHashCode() 
                + Iat.GetHashCode()
                + Exp.GetHashCode();
        }
    }
}
