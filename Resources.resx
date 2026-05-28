using System.ComponentModel.DataAnnotations.Schema;
using Sklad1.Data;

namespace Sklad1.Models
{
    /// <summary>
    /// Лог заблокированных попыток оформления отгрузки или поставки.
    /// </summary>
    [Table("blocked_operation_logs")]
    public class BlockedOperationLog
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("document_type")]
        public string DocumentType { get; set; } = string.Empty;

        [Column("inn")]
        public string? Inn { get; set; }

        [Column("reason")]
        public string Reason { get; set; } = string.Empty;

        [Column("attempted_at")]
        public DateTime AttemptedAt { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
