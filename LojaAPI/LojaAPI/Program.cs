using LojaAPI.Repositories;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<UsuarioRepository>(provider => new UsuarioRepository(builder.Configuration));
builder.Services.AddScoped<ProdutoRepository>(provider => new ProdutoRepository(builder.Configuration));
builder.Services.AddScoped<CarrinhoRepository>(provider => new CarrinhoRepository(builder.Configuration));
builder.Services.AddScoped<PedidoRepository>(provider => new PedidoRepository(builder.Configuration));
builder.Services.AddScoped<PedidoProdutoRepository>(provider => new PedidoProdutoRepository(builder.Configuration));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Loja API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
