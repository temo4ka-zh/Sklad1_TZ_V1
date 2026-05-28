using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad1.Models
{
    /// <summary>
    /// Модель настроек приложения
    /// </summary>
    [Table("settings")]
    public class Setting
    {
        /// <summary>
        /// Уникальный идентификатор настроек (всегда одна запись)
        /// </summary>
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Валюта отображения для пользователя (RUB, USD, EUR, CNY)
        /// </summary>
        [Column("display_currency")]
        public string DisplayCurrency { get; set; } = "RUB";

        /// <summary>
        /// Количество дней до истечения срока годности для жёлтой подсветки (предупреждение)
        /// </summary>
        [Column("expiry_warning_days")]
        public int ExpiryWarningDays { get; set; } = 7;

        /// <summary>
        /// Количество дней до истечения срока годности для оранжевой подсветки (опасно)
        /// </summary>
        [Column("expiry_danger_days")]
        public int ExpiryDangerDays { get; set; } = 3;

        /// <summary>
        /// Дата и время последнего обновления курсов валют
        /// </summary>
        [Column("last_rates_update")]
        public DateTime? LastRatesUpdate { get; set; }
    }
}
