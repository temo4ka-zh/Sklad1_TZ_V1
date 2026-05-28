using Sklad1.Data;
using System;

namespace Sklad1.Models
{
    /// <summary>
    /// Хранит данные текущего вошедшего пользователя
    /// </summary>
    public static class CurrentUser
    {
        /// <summary>
        /// Уникальный идентификатор пользователя (GUID)
        /// </summary>
        public static Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public static string FirstName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public static string LastName { get; set; }

        /// <summary>
        /// Электронная почта пользователя (логин)
        /// </summary>
        public static string Email { get; set; }

        /// <summary>
        /// Роль пользователя (Администратор/Кладовщик)
        /// </summary>
        public static UserRole Role { get; set; }
    }
}