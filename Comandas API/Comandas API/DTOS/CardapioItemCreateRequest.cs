namespace Comandas_API.DTOS
{
    public class CardapioItemCreateRequest
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public decimal Preco { get; set; }
        public bool PossuiPreparo { get; set; }
        public int? CategoriaCardapioId { get; set; }
    }
}
