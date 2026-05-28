namespace Sklad1.Models
{
    /// <summary>
    /// Модель товара для выпадающего списка в форме отгрузки
    /// </summary>
    public class ProductItem
    {
        /// <summary>
        /// Артикул товара
        /// </summary>
        public string Article { get; set; }

        /// <summary>
        /// Название товара
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Количество на складе
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Цена закупки
        /// </summary>
        public decimal PurchasePrice { get; set; }
    }
}