using CarRental.Repositories.Context;
using CarRental.Repositories.Interfaces;
using CarRental.Repositories.Repositories;
using CarRental.Services.Interfaces;
using CarRental.Services.Mapping;
using CarRental.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Swagger/OpenAPI ayarlar�
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// InMemory Database yap�land�rmas�
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("CarRentalDb"));

// Repository ve Service kay�tlar� (Scoped)
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();

// AutoMapper yap�land�rmas�
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Geli�tirme ortam�nda Swagger kullan�m�
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed Data eklemesi (veritaban� ba�lang�� verisi)
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Cars.AddRange(
        new CarRental.Domain.Entities.Car { Brand = "BMW", Model = "320i", IsAvailable = true },
        new CarRental.Domain.Entities.Car { Brand = "Audi", Model = "A4", IsAvailable = false }
    );

    context.SaveChanges();
}

app.Run();
