using AutoMapper;
using CarRental.Domain.Entities;
using CarRental.Repositories.Interfaces;
using CarRental.Services.Dtos;
using CarRental.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<List<Car>> GetAllCarsAsync()
        {
            return await _carRepository.GetAllAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _carRepository.GetByIdAsync(id);
        }

        public async Task<bool> AddCarAsync(Car car)
        {
            await _carRepository.AddAsync(car);
            return await _carRepository.SaveChangesAsync();
        }

        public async Task<bool> UpdateCarAsync(Car car)
        {
            _carRepository.Update(car);
            return await _carRepository.SaveChangesAsync();
        }

        public async Task<bool> DeleteCarAsync(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null)
                return false;

            _carRepository.Delete(car);
            return await _carRepository.SaveChangesAsync();
        }
    }
}
