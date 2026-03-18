using System.Security.Cryptography.Xml;
using System.Text;
using System.Text.Json.Serialization;
using ef_nortwith.dbContext;
using ef_nortwith.interfacez;
using ef_nortwith.repositorio;
using ef_nortwith.services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

string key = "kn5ln23nm4jn5kj43n1kn43325nkj6543";

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    var signinkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
    var signinCredemtial = new SigningCredentials(signinkey, SecurityAlgorithms.HmacSha256Signature);
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = signinkey,
    };
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<NorthwindContext>(opc =>{
     opc.UseNpgsql(builder.Configuration.GetConnectionString("postgres"));
});
builder.Services.AddTransient<ProductsServices>();
builder.Services.AddTransient<IRepositorioProdcutos,RepositorioProductos>();
builder.Services.AddTransient<IRepositorioOrdenes, RepositorioOrdenes>();
builder.Services.AddTransient<OrdenesServices>();
builder.Services.AddTransient<RepositorioProveedores>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<UsuarioService>();
builder.Services.AddTransient<IRepositorioEmpleados, RepositorioEmpleados>();
builder.Services.AddTransient<EmpleadosServices>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
