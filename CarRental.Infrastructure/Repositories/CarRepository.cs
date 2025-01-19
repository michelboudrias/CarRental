using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Persistence;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories;

public class CarRepository : ICarRepository
{
    public CarRentalDbContext Context { get; }

    public CarRepository(CarRentalDbContext context)
    {
        Context = context;
    }

    public async Task<Result<Car>> GetByRegistrationNumberAsync(string registrationNumber)
    {
        try
        {
            var car = await Context.Cars.SingleOrDefaultAsync(c =>
                c.RegistrationNumber.Equals(registrationNumber, StringComparison.InvariantCultureIgnoreCase));
            return Result.Ok(car);
        }

        catch (Exception e)
        {
            return Result.Fail<Car>(e.Message);
        }
    }
}