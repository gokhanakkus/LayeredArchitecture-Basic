using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
    public class CartItemEntity
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int CartId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required, Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [ForeignKey(nameof(CartId))]
        public virtual CartEntity Cart { get; set; }//miras alan sınıflar yeniden uygulayabilir değiştirebilir.

        [ForeignKey(nameof(ProductId))]
        public virtual ProductEntity Product { get; set; }
     
    }
}