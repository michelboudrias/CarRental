using CarRental.Domain.Entities;

namespace CarRental.Infrastructure.Persistence;

public static class CarRentalDbSeeder
{
    public static Task SeedAsync(CarRentalDbContext db)
    {
        AddCarCategories(db);
        AddCars(db);
        AddPricingDefinitions(db);
        AddOngoingRentals(db);
        return db.SaveChangesAsync();
    }

    private static void AddCarCategories(CarRentalDbContext db)
    {
        db.CarCategories.Add(new CarCategory
        {
            Id = 1,
            Name = "SmallCar",
        });
        db.CarCategories.Add(new CarCategory
        {
            Id = 2,
            Name = "Combi",
        });
        db.CarCategories.Add(new CarCategory
        {
            Id = 3,
            Name = "Truck",
        });
    }

    private static void AddCars(CarRentalDbContext db)
    {
        db.Cars.Add(new Car
        {
            Id = 1,
            RegistrationNumber = "ABC123",
            CarCategoryId = 1
        });
        db.Cars.Add(new Car
        {
            Id = 2,
            RegistrationNumber = "DEF456",
            CarCategoryId = 2
        });
        db.Cars.Add(new Car
        {
            Id = 3,
            RegistrationNumber = "GHI789",
            CarCategoryId = 3
        });
    }

    private static void AddPricingDefinitions(CarRentalDbContext db)
    {
        db.PricingDefinitions.Add(new PricingDefinition
        {
            Id = 1,
            Name = "Small Car Formula",
            CarCategoryId = 1,
            PricePerDay = 500,
            PricePerDayMultiplier = 1,
            PricePerKilometer = 0,
            PricePerKilometerMultiplier = 0,
        });
        db.PricingDefinitions.Add(new PricingDefinition
        {
            Id = 2,
            Name = "Combi Formula",
            CarCategoryId = 2,
            PricePerDay = 500,
            PricePerDayMultiplier = 1.3m,
            PricePerKilometer = 0.5m,
            PricePerKilometerMultiplier = 1,
        });
        db.PricingDefinitions.Add(new PricingDefinition
        {
            Id = 3,
            Name = "Truck Formula",
            CarCategoryId = 3,
            PricePerDay = 500,
            PricePerDayMultiplier = 1.5m,
            PricePerKilometer = 0.5m,
            PricePerKilometerMultiplier = 1.5m
        });
    }

    private static void AddOngoingRentals(CarRentalDbContext db)
    {
        db.Rentals.Add(new Rental
        {
            BookingNumber = 1,
            Start = DateTime.Parse("2025-01-01T14:35:00"),
            CarId = 1,
            DriverSSN = "197709169861",
            OdometerReadingAtStart = 10000
        });
        db.Rentals.Add(new Rental
        {
            BookingNumber = 2,
            Start = DateTime.Parse("2025-01-01T14:36:00"),
            CarId = 2,
            DriverSSN = "198809244567",
            OdometerReadingAtStart = 20000
        });
        db.Rentals.Add(new Rental
        {
            BookingNumber = 3,
            Start = DateTime.Parse("2025-01-01T14:37:00"),
            CarId = 3,
            DriverSSN = "199903249812",
            OdometerReadingAtStart = 30000
        });
    }
}