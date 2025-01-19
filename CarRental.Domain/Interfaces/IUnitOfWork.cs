namespace CarRental.Domain.Interfaces;

public interface IUnitOfWork
{
    ICarRepository CarRepository { get; }
    IPricingDefinitionRepository PricingDefinitionRepository { get; }
    IRentalRepository RentalRepository { get; }
}