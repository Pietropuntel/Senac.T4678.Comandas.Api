using Comandas_API;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ComandasDbContext>(opition =>
opition.UseSqlite("DataSource=comandas.db"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.UseSwagger();
    app.UseSwaggerUI();
//}
// criar banco de dados
// criar um scopo usado para obter instancias de variaveis
using (var scope = app.Services.CreateScope())
{
    // obtem um objeto do banco de dados 
    var db = scope.ServiceProvider.GetRequiredService<ComandasDbContext>();
    // executa as migrações no banco de dados 
    await db.Database.MigrateAsync();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
