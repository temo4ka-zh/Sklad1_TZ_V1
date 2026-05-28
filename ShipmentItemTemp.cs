using System.ComponentModel.DataAnnotations.Schema;

namespace Sklad1.Models
{
    /// <summary>
    /// Модель партии товара (для учёта сроков годности)
    /// </summary>
    [Table("product_batches")]
    public class ProductBatch
    {
        /// <summary>
        /// Уникальный идентификатор партии
        /// </summary
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        [Column("product_id")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Идентификатор поставки, по которой пришла партия
        /// </summary>
        [Column("supply_id")]
        public Guid? SupplyId { get; set; }

        /// <summary>
        /// Количество товара в партии
        /// </summary>
        [Column("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Закупочная цена товара в этой партии
        /// </summary>
        [Column("purchase_price")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Срок годности партии
        /// </summary>
        [Column("expiry_date")]
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Статус партии (active - активна, expired - просрочена, closed - закрыта)
        /// </summary>
        [Column("status")]
        public string Status { get; set; } = "active";

        /// <summary>
        /// Код ячейки склада для тепловой карты (например, A1, B2).
        /// </summary>
        [Column("cell_code")]
        public string? CellCode { get; set; }

        // Связи
        /// <summary>
        /// Связанный товар
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Связанная поставка
        /// </summary>
        public virtual Supply Supply { get; set; }
    }
}