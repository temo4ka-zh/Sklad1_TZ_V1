using System.ComponentModel.DataAnnotations.Schema;

namespace Sklad1.Models
{
    /// <summary>
    /// Локальный черный список ИНН.
    /// </summary>
    [Table("blacklisted_inns")]
    public class BlacklistedInn
    {
        [Column("inn")]
        public string Inn { get; set; } = string.Empty;

        [Column("reason")]
        public string? Reason { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}
