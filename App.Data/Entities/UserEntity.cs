using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Data.Entities
{
    public class UserEntity
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required, MinLength(3), MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        [Required, MinLength(3), MaxLength(50)]
        public string Surname { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Password), MinLength(4)]
        public string Password { get; set; } = string.Empty;

        public virtual ICollection<CartEntity> Carts { get; set; }//miras alan sınıflar yeniden uygulayabilir değiştirebilir.

    }
}