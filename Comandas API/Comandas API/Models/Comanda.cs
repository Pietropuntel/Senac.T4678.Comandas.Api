using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comandas_API.Models
{
    public class Comanda
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int NumeroMesa { get; set; } = default!;
        public string NomeCliente { get; set; } = default!;
        public List<ComandaItem> Itens { get; set; } = new List<ComandaItem>() { }; 

    }
}
