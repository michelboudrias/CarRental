using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.Entities;

public class PricingDefinition
{
    public int Id { get; set; }
    public string Name { get; set; }
    [ForeignKey("CarCategory")] public int CarCategoryId { get; set; }
    public CarCategory CarCategory { get; set; }
    public decimal PricePerDay { get; set; }
    public decimal PricePerKilometer { get; set; }
    public decimal PricePerDayMultiplier { get; set; }
    public decimal PricePerKilometerMultiplier { get; set; }

    //currency?
}