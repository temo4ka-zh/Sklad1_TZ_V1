using System.ComponentModel.DataAnnotations.Schema;

namespace Sklad1.Models
{
    /// <summary>
    /// Модель категории товаров
    /// </summary>
    [Table("categories")]
    public class Category
    {
        /// <summary>
        /// Идентификатор категории
        /// </summary>
        [Column("id")]
        public Guid Id { get; set; }

        /// <summary>
        /// Название категории
        /// </summary>
        [Column("name")]
        public string Name { get; set; }

        /// <summary>
        /// Описание категории
        /// </summary>
        [Column("description")]
        public string Description { get; set; }
    }
}
