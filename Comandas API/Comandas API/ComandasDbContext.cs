using Microsoft.EntityFrameworkCore;

namespace Comandas_API;


public class ComandasDbContext : DbContext
{
    public ComandasDbContext(DbContextOptions<ComandasDbContext> options
    ) : base(options)
    {
    }
    //definir algumas configurações adicionais no banco 
    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Models.Usuario>()
            .HasData(
            new Models.Usuario
            {
                Id = 1,
                Nome = "Admin",
                Email = "admin@gmail.com",
                Senha = "admin123"

            });
        modelBuilder.Entity<Models.Mesa>()
            .HasData(
            new Models.Mesa
            {
                Id = 1,
                NumeroMesa = 1,
                SituacaoMesa = 0
            },
            new Models.Mesa
            {
                Id = 2,
                NumeroMesa = 2,
                SituacaoMesa = 0
            },
            new Models.Mesa
            {
                Id = 3,
                NumeroMesa = 3,
                SituacaoMesa = 0
            });
        modelBuilder.Entity<Models.CardapioItem>()
            .HasData(
            new Models.CardapioItem
            {
                Id = 1,
                Titulo = "Coxinha",
                Descricao = "Coxinha de frango com catupiry",
                Preco = 6.50m,
                PossuiPreparo = true
            },
            new Models.CardapioItem
            {
                Id = 2,
                Titulo = "Refrigerante",
                Descricao = "Refrigerante lata 350ml",
                Preco = 5.00m,
                PossuiPreparo = false
            },
            new Models.CardapioItem
            {
                Id = 3,
                Titulo = "Pizza",
                Descricao = "Pizza de calabresa grande",
                Preco = 35.00m,
                PossuiPreparo = true
            });
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Models.Usuario> Usuarios { get; set; } = default!;
    public DbSet<Models.Mesa> Mesas { get; set; } = default!;
    public DbSet<Models.Reserva> Reservas { get; set; } = default!;
    public DbSet<Models.Comanda> comandas { get; set; } = default!;
    public DbSet<Models.ComandaItem> comandaItems { get; set; } = default!;
    public DbSet<Models.PedidoCozinha> PedidoCozinhas { get; set; } = default!;
    public DbSet<Models.CardapioItem> CardapioItens { get; set; } = default!;
    public DbSet<Models.PedidoCozinhaItem> PedidoCozinhasItens { get; set; } = default!;
}
