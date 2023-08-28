using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace App.Data.Entities
{
    public class CartEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; }

        [Required]
        public string Name { get; set; }//cart name||sepet ismi
        public virtual ICollection<CartItemEntity> CartItems { get; set; }//miras alan sınıflar yeniden uygulayabilir değiştirebilir.

    }
}