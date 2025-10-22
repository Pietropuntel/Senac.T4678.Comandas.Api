using Microsoft.EntityFrameworkCore;

namespace Comandas_API;


public class ComandasDbContext : DbContext
{
    public ComandasDbContext(DbContextOptions<ComandasDbContext> options
    ) : base(options)
    {
    }
    public DbSet<Models.Usuario> Usuarios { get; set; } = default!;
    public DbSet<Models.Mesa> Mesas { get; set; } = default!;
    public DbSet<Models.Reserva> Reservas { get; set; } = default!;
    public DbSet<Models.Comanda> comandas { get; set; } = default!;
    public DbSet<Models.ComandaItem> comandaItems { get; set; } = default!;
    public DbSet<Models.PedidoCozinha> PedidoCozinhas { get; set; } = default!;
    public DbSet<Models.CardapioItem> CardapioItens { get; set; } = default!;
}
