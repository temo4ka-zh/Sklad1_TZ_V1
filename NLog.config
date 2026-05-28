using Sklad1.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklad1.Models
{
    /// <summary>
    /// Модель пользователя
    /// </summary>
    [Table("users")]
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        [Column("last_name")]
        public string LastName { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Column("first_name")]
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        [Column("middle_name")]
        public string MiddleName { get; set; }

        /// <summary>
        /// Почта (логин) пользователя
        /// </summary>
        [Column("email")]
        public string Email { get; set; }

        /// <summary>
        /// Хэш пароля
        /// </summary>
        [Column("password_hash")]
        public string PasswordHash { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        [Column("role")]
        public UserRole Role { get; set; }
    }
}
