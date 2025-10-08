﻿namespace Comandas_API.DTOS
{
    public class CardapioItemUpdateRequest
    {
        public string Titulo { get; set; } = default;
        public string Descricao { get; set; } = default;
        public decimal Preco { get; set; }
        public bool PossuiPreparo { get; set; }
    }
}
