using CarRental.Domain.Interfaces;

namespace CarRental.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    public UnitOfWork(ICarRepository carRepository,
        IPricingDefinitionRepository pricingDefinitionRepository, IRentalRepository rentalRepository)
    {
        CarRepository = carRepository;
        PricingDefinitionRepository = pricingDefinitionRepository;
        RentalRepository = rentalRepository;
    }

    public ICarRepository CarRepository { get; }
    public IPricingDefinitionRepository PricingDefinitionRepository { get; }
    public IRentalRepository RentalRepository { get; }
}