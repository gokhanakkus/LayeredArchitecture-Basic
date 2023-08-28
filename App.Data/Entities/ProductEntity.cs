using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; } = string.Empty;

        public virtual ICollection<CartItemEntity> CartItems { get; set; }//miras alan sınıflar yeniden uygulayabilir değiştirebilir.

    }
}