using System.ComponentModel.DataAnnotations.Schema;

namespace Sklad1.Models
{
    /// <summary>
    /// Модель товаров
    /// </summary>
    [Table("products")]
    public class Product
    {
        /// <summary>
        /// Идентифактор товара
        /// </summary>
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Артикул товара
        /// </summary>
        [Column("article")]
        public string Article { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Идентификатор категории
        /// </summary>
        [Column("category_id")]
        public Guid CategoryId { get; set; }

        /// <summary>
        /// Себестоимость
        /// </summary>
        [Column("purchase_price")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Количество на складе
        /// </summary>
        [Column("quantity")]
        public int Quantity { get; set; }

        [Column("initial_quantity")]
        public int InitialQuantity { get; set; }

        /// <summary>
        /// Единица измерения
        /// </summary>
        [Column("unit")]
        public string Unit {  get; set; }

        /// <summary>
        /// Коллекция позиций отгрузок для этого товара
        /// </summary>
        public virtual ICollection<ShipmentItem> ShipmentItems { get; set; }
    }
}
