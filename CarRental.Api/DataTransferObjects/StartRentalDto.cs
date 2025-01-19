using System.ComponentModel.DataAnnotations;

namespace CarRental.Api.DataTransferObjects;

public class StartRentalDto
{
    [Required(AllowEmptyStrings = false)] public string CarRegistrationNumber { get; set; }
    [Required(AllowEmptyStrings = false)] public string DriverSSN { get; set; }
    [Required] public DateTime Start { get; set; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Odometer reading at return must be greater than 0.")]
    public int OdometerReadingAtStart { get; set; }
}