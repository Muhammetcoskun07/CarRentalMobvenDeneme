using CarRental.Domain.Entities;
using CarRental.Services.Dtos;
using CarRental.Services.Interfaces;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace CarRental.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;

    public CarsController(ICarService carService)
    {
        _carService = carService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Car>>> GetAll()
    {
        var cars = await _carService.GetAllCarsAsync();
        return Ok(cars);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Car>> GetById(int id)
    {
        var car = await _carService.GetCarByIdAsync(id);
        if (car == null)
            return NotFound();

        return Ok(car);
    }

    [HttpPost]
    public async Task<ActionResult> Create(Car car)
    {
        var result = await _carService.AddCarAsync(car);
        if (!result)
            return BadRequest("Car could not be added.");

        return CreatedAtAction(nameof(GetById), new { id = car.Id }, car);
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Car car)
    {
        if (id != car.Id)
            return BadRequest("Id mismatch");

        try
        {
            var existingCar = await _carService.GetCarByIdAsync(id);
            if (existingCar == null)
                return NotFound();

            existingCar.Brand = car.Brand;
            existingCar.Model = car.Model;
            existingCar.IsAvailable = car.IsAvailable;

            var result = await _carService.UpdateCarAsync(existingCar);
            if (!result)
                return BadRequest("Update failed");

            return NoContent();
        }
        catch (Exception ex)
        {
            // Log exception ex
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

}

