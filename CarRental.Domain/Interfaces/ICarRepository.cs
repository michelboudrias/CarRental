using CarRental.Domain.Entities;
using FluentResults;

namespace CarRental.Domain.Interfaces;

public interface ICarRepository
{
    Task<Result<Car>> GetByRegistrationNumberAsync(string registrationNumber);
}