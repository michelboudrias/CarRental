using CarRental.Domain.Entities;
using CarRental.Domain.Interfaces;
using CarRental.Infrastructure.Persistence;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Infrastructure.Repositories;

internal class RentalRepository : IRentalRepository
{
    private readonly CarRentalDbContext _context;

    public RentalRepository(CarRentalDbContext context)
    {
        _context = context;
    }

    public async Task<Result<Rental>> GetByIdAsync(int id)
    {
        try
        {
            var rental = await _context.Rentals
                .Include(r => r.Car)
                .ThenInclude(c => c.Category)
                .SingleOrDefaultAsync(r => r.BookingNumber.Equals(id));
            return Result.Ok(rental);
        }
        catch (Exception e)
        {
            return Result.Fail<Rental>(new Error(e.Message));
        }
    }

    public async Task<Result<int>> AddAsync(Rental rental)
    {
        try
        {
            var rentalEntity = await _context.Rentals.AddAsync(rental);
            await _context.SaveChangesAsync();
            return Result.Ok(rentalEntity.Entity.BookingNumber);
        }
        catch (Exception e)
        {
            return Result.Fail<int>(new Error(e.Message));
        }
    }

    public async Task<Result<Rental>> UpdateAsync(Rental rental)
    {
        try
        {
            var rentalEntity = _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();

            return Result.Ok(rentalEntity.Entity);
        }
        catch (Exception e)
        {
            return Result.Fail<Rental>(new Error(e.Message));
        }
    }
}