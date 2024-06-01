using Core.Domain;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infrastructure.Entities
{
    public class ProdutosEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MinLength(3)]
        public string Name {get; set; }

        [Required]
        public float Value { get; set; }

        public virtual List<Pedidos> Pedidos { get; set; }

        public ProdutosEntity(string name, float value)
        {
            Name = name;
            Value = value;
        }
    }
}
