using CarRental.Repositories.Context;
using CarRental.Repositories.Interfaces;
using CarRental.Repositories.Repositories;
using CarRental.Services.Interfaces;
using CarRental.Services.Mapping;
using CarRental.Services.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseInMemoryDatabase("CarRentalDb"));

builder.Services.AddScoped<ICarRepository, CarRepository>();
builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    context.Cars.AddRange(
        new CarRental.Domain.Entities.Car { Brand = "BMW", Model = "320i", IsAvailable = true },
        new CarRental.Domain.Entities.Car { Brand = "Audi", Model = "A4", IsAvailable = false }
    );

    context.Users.Add(
        new CarRental.Domain.Entities.User { Email = "admin@admin.com", Password = "123456" }
    );

    context.SaveChanges();
}

app.Run();
