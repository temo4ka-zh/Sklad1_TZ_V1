using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sklad1.Models
{
    /// <summary>
    /// Модель убытков (списанные просроченные товары)
    /// </summary>
    [Table("losses")]
    public class Loss
    {
        /// <summary>
        /// Уникальный идентификатор записи об убытке
        /// </summary>
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор списанного товара
        /// </summary>

        [Column("product_id")]
        public Guid ProductId { get; set; }

        /// <summary>
        /// Количество списанного товара
        /// </summary>
        [Column("quantity")]
        public int Quantity { get; set; }

        /// <summary>
        /// Закупочная цена списанного товара
        /// </summary>
        [Column("purchase_price")]
        public decimal PurchasePrice { get; set; }

        /// <summary>
        /// Тип убытка (expired - просрочка, damaged - порча, manual - ручное списание)
        /// </summary>
        [Column("type")]
        public string Type { get; set; } = "expired";

        /// <summary>
        /// Дата списания товара
        /// </summary>
        [Column("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Связанный товар
        /// </summary>
        public virtual Product Product { get; set; }
    }
}
