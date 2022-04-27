using gestorpredialsys.entidades;
using gestorpredialsys.webapi.Repositorios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IMoradorRepositorio, MoradorRepositorio>();
builder.Services.AddScoped<ICondominioRepositorio, CondominioRepositorio>();
builder.Services.AddScoped<IFamiliaRepositorio, FamiliaRepositorio>();
builder.Services.AddDbgestaopredialContexto();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
