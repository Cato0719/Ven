using Microsoft.EntityFrameworkCore;
using Ven.AccessData.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Se agrego despues de instalar Swashbucle.AspNetCore
builder.Services.AddSwaggerGen();

//Conexion a la base de datos 
builder.Services.AddDbContext<DataContext>(x => 
x.UseSqlServer("name=DefaultConnection ", options => options.MigrationsAssembly("Ven.Backend")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();//Activa OpenAPI para documentar la API.
    //Habilitan Swagger para ver y probar la api
    app.UseSwagger();
    app.UseSwaggerUI();

    //se agrego para OpenBrowser
    var swaggerUrl = "https://localhost:7251/swagger";//url de swagger //Guarda la URL de Swagger.
    Task.Run(()=>OpenBrowser(swaggerUrl)); //Abre la URL de Swagger automáticamente
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//Esta función abre automáticamente el navegador y carga la URL que le pases
static void OpenBrowser(string url)
{
    try
    {
        var psi = new System.Diagnostics.ProcessStartInfo
        {
            FileName = url,
            UseShellExecute = true
        };

        System.Diagnostics.Process.Start(psi);
    }
    catch(Exception ex)
    {
        Console.WriteLine($"Error al abrir el navegador: {ex.Message}");
    }
}