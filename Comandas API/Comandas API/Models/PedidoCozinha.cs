using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comandas_API.Models
{
    public class PedidoCozinha
    {
        [Key]// PK
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ComandaId { get; set; } //FK
        public virtual Comanda Comanda { get; set; } // NAVEGAÇÃO
        public List<PedidoCozinhaItem> itens { get; set; } = [];
       
    }
}
