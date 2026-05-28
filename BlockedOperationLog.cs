using System.ComponentModel.DataAnnotations.Schema;

namespace Sklad1.Models
{
    /// <summary>
    /// Модель поставки товаров
    /// </summary>
    [Table("supplies")]
    public class Supply
    {
        /// <summary>
        /// Уникальный идентификатор поставки
        /// </summary>
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор пользователя, принявшего поставку
        /// </summary>
        [Column("user_id")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Наименование поставщика
        /// </summary>
        [Column("supplier")]
        public string Supplier { get; set; }

        /// <summary>
        /// Дата и время приёма поставки
        /// </summary>
        [Column("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Источник поставки (manual - ручной ввод, import_csv - импорт CSV, import_xlsx - импорт Excel)
        /// </summary>
        [Column("source")]
        public string Source { get; set; }

        /// <summary>
        /// Связанный пользователь
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Коллекция позиций поставки
        /// </summary>
        public virtual ICollection<SupplyItem> SupplyItems { get; set; }
    }
}