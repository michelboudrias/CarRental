using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.Entities;

public class Car
{
    public int Id { get; set; }
    public string RegistrationNumber { get; set; }
    [ForeignKey("CarCategory")] public int CarCategoryId { get; set; }
    public CarCategory Category { get; set; }
}