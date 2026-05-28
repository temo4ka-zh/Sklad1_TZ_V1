using System.ComponentModel.DataAnnotations.Schema;
using Sklad1.Data;

namespace Sklad1.Models
{
    /// <summary>
    /// Результат проверки погоды для отгрузки.
    /// </summary>
    [Table("weather_checks")]
    public class WeatherCheck
    {
        [Column("id")]
        public Guid Id { get; set; }

        [Column("shipment_id")]
        public Guid? ShipmentId { get; set; }

        [Column("city")]
        public string City { get; set; } = string.Empty;

        [Column("delivery_date")]
        public DateTime DeliveryDate { get; set; }

        [Column("temperature")]
        public double Temperature { get; set; }

        [Column("precipitation")]
        public double Precipitation { get; set; }

        [Column("wind_speed")]
        public double WindSpeed { get; set; }

        [Column("risk_level")]
        public string RiskLevel { get; set; } = string.Empty;

        [Column("recommendation")]
        public string Recommendation { get; set; } = string.Empty;

        [Column("checked_at")]
        public DateTime CheckedAt { get; set; }

        [Column("user_id")]
        public Guid UserId { get; set; }

        public virtual User? User { get; set; }
    }
}
