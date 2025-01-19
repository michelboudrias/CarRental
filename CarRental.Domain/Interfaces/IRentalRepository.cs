using CarRental.Domain.Entities;
using FluentResults;

namespace CarRental.Domain.Interfaces;

public interface IRentalRepository
{
    Task<Result<Rental>> GetByIdAsync(int id);
    Task<Result<int>> AddAsync(Rental rental);
    Task<Result<Rental>> UpdateAsync(Rental rental);
}