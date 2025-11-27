namespace Comandas_API.DTOS
{
    public class ComandaUpdateRequest
    {
        public int NumeroMesa { get; set; } = default!;
        public string NomeCliente { get; set; } = default!;
        public ComandaItemUpdateRequest[] Itens { get; set; } = [];// lista
    }
    public class ComandaItemUpdateRequest
    {
        public int Id { get; set; } // idda comanda item
        public bool Remove { get; set; }//indicar se esta removendo
        public int CardapioItemId { get; set; } // indicar se estar inserindo 
    }
}
