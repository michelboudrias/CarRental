using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Domain.Entities;

public class Rental
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int BookingNumber { get; set; }

    [ForeignKey("Car")] public int CarId { get; set; }
    public Car Car { get; set; }
    public string DriverSSN { get; set; }
    public DateTime Start { get; set; }
    public DateTime? End { get; set; }
    public int OdometerReadingAtStart { get; set; }
    public int? OdometerReadingAtReturn { get; set; }
    public decimal? Price { get; set; }
}