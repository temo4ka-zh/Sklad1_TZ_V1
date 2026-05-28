using System.ComponentModel.DataAnnotations.Schema;

namespace Sklad1.Models
{
    /// <summary>
    /// Модель позиции поставки
    /// </summary>
    [Table("supply_items")]
    public class SupplyItem
    {
        /// <summary>
        /// Уникальный идентификатор позиции поставки
        /// </summary>
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор поставки
        /// </summary>
        [Column("supply_id")]
        public Guid SupplyId { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        [Column("product_id")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Идентификатор партии товара (срок годности)
        /// </summary>
        [Column("batch_id")]
        public Guid BatchId { get; set; }

        /// <summary>
        /// Количество принятого товара
        /// </summary>
        [Column("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Закупочная цена товара в этой позиции
        /// </summary>
        [Column("purchase_price")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Связанные поставка, товар и партия товара
        /// </summary>
        public virtual Supply Supply { get; set; }
        public virtual Product Product { get; set; }
        public virtual ProductBatch Batch { get; set; }
    }
}