using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Publisher;

public class PublisherRequestDto
{
    [Required]
    public required string CompanyName { get; set; }
    public string? HomePage { get; set; }
    public string? Description { get; set; }
}