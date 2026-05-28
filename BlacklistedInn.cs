namespace Sklad1.Helpers
{
    /// <summary>
    /// Временная модель для отображения в таблице отгрузки
    /// </summary>
    public class ShipmentItemTemp
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
        /// Количество для отгрузки
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Цена товара на момент отгрузки
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Получатель
        /// </summary>
        public string Client { get; set; }
    }
}