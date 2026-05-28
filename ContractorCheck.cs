using Sklad1.Data;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sklad1.Models
{
    /// <summary>
    /// Модель позиции отгрузки
    /// </summary>
    [Table("shipment_items")]
    public class ShipmentItem
    {
        /// <summary>
        /// Идентификатор позиции
        /// </summary>
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор отгрузки
        /// </summary>
        [Column("shipment_id")]
        public Guid ShipmentId { get; set; }

        /// <summary>
        /// Идентификатор товара
        /// </summary>
        [Column("product_id")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Количество отгружаемого товара
        /// </summary>
        [Column("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Цена на момент отгрузки
        /// </summary>
        [Column("price_at_shipment")]
        public decimal PriceAtShipment { get; set; }

        /// <summary>
        /// Себестоимость товара на момент отгрузки (фиксируется для расчёта прибыли)
        /// </summary>
        [Column("cost_at_shipment")]
        public decimal CostAtShipment { get; set; }

        /// <summary>
        /// Связанная отгрузка
        /// </summary>
        public virtual Shipment Shipment { get; set; }

        /// <summary>
        /// Связанный товар
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
