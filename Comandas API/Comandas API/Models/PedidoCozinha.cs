﻿namespace Comandas_API.Models
{
    public class PedidoCozinha
    {
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public List<PedidoCozinhaItem> itens { get; set; } = [];
       
    }
}
