using System.Security.Cryptography.Xml;
using System.Text.Json.Serialization;
using ef_nortwith.dbContext;
using ef_nortwith.repositorio;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NorthwindContext>(opc =>{
     opc.UseNpgsql(builder.Configuration.GetConnectionString("postgres"));
});
builder.Services.AddTransient<ProductsServices>();
builder.Services.AddTransient<IRepositorioProdcutos,RepositorioProductos>();
builder.Services.AddTransient<RepositorioOrdenes>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
