using CarRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Repositories.Interfaces
{
  
    public interface ICarRepository
    {
        Task<List<Car>> GetAllAsync();
        Task<Car> GetByIdAsync(int id);
        Task AddAsync(Car car);
        void Update(Car car);
        void Delete(Car car);
        Task<bool> SaveChangesAsync();
    }
}

