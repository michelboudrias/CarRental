using System.ComponentModel.DataAnnotations;

namespace CarRental.Api.DataTransferObjects;

public class EndRentalDto
{
    [Required] public DateTime End { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Odometer reading at return must be greater than 0.")]
    public int OdometerReadingAtReturn { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Price definition ID must be greater than 0.")]
    public int PriceDefinitionId { get; set; }
}