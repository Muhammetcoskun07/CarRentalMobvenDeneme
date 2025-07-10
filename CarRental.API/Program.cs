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

// Swagger/OpenAPI ayarlarý
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// InMemory Database yapýlandýrmasý
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("CarRentalDb"));

// Repository ve Service kayýtlarý (Scoped)
builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();

// AutoMapper yapýlandýrmasý
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

// Geliþtirme ortamýnda Swagger kullanýmý
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

// Seed Data eklemesi (veritabaný baþlangýç verisi)
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
