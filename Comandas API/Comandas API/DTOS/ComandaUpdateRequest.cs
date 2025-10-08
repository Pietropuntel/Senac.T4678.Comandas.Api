namespace Comandas_API.DTOS
{
    public class ComandaUpdateRequest
    {
        public int NumeroMesa { get; set; } = default!;
        public string NomeCliente { get; set; } = default!;
        public int[] CardapioItemIds { get; set; } = default!;
    }
}
