using System.Security.Cryptography;
using System.Text;

namespace Sklad1.Helpers
{
    /// <summary>
    /// Вспомогательный класс для работы с паролями
    /// </summary>
    public static class Password
    {
        /// <summary>
        /// Преобразует пароль в хэш с использованием алгоритма SHA256
        /// </summary>
        public static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
