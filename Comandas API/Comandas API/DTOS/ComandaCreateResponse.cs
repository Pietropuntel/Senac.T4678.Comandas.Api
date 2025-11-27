using Comandas_API.Models;

namespace Comandas_API.DTOS
{
    public class ComandaCreateResponse
    {
        public int Id { get; set; }
        public int NumeroMesa { get; set; } = default!;
        public string NomeCliente { get; set; } = default!;
        public List<ComandaItemResponse> Itens { get; set; } = new List<ComandaItemResponse>() { };
    }
    public class ComandaItemResponse
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = default!;
       
    }
}
