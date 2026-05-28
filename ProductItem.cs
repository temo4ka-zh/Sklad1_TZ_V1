using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad1.Models
{
    /// <summary>
    /// Модель курса валюты
    /// </summary>
    [Table("currency_rates")]
    public class CurrencyRate
    {
        /// <summary>
        /// Уникальный идентификатор записи курса валюты
        /// </summary>
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Код валюты (RUB, USD, EUR, CNY)
        /// </summary>
        [Column("code")]
        public string Code { get; set; }

        /// <summary>
        /// Курс валюты по отношению к рублю
        /// </summary>
        [Column("rate_to_rub")]
        public decimal RateToRub { get; set; }

        /// <summary>
        /// Дата и время последнего обновления курса
        /// </summary>
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; }
    }
}
