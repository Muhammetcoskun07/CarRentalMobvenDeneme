using CarRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRental.Services.Interfaces
{
    public interface IAuthService
    {
        Task<User> LoginAsync(string email, string password);
    }

}
