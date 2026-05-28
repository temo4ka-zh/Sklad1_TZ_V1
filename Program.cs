using System.ComponentModel.DataAnnotations.Schema;
using Sklad1.Data;

namespace Sklad1.Models
{
    /// <summary>
    /// Результат проверки ИНН контрагента или поставщика.
    /// </summary>
    [Table("contractor_checks")]
    public class ContractorCheck
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("inn")]
        public string Inn { get; set; } = string.Empty;

        [Column("organization_name")]
        public string OrganizationName { get; set; } = string.Empty;

        [Column("external_status")]
        public string ExternalStatus { get; set; } = string.Empty;

        [Column("result_status")]
        public string ResultStatus { get; set; } = string.Empty;

        [Column("checked_at")]
        public DateTime CheckedAt { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        [Column("document_type")]
        public string DocumentType { get; set; } = string.Empty;

        [Column("shipment_id")]
        public Guid? ShipmentId { get; set; }

        [Column("supply_id")]
        public Guid? SupplyId { get; set; }

        [Column("is_success")]
        public bool IsSuccess { get; set; }

        [Column("error_message")]
        public string? ErrorMessage { get; set; }

        public virtual User? User { get; set; }
    }
}
