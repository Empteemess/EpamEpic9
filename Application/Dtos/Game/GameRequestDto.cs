using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Game;

public class GameRequestDto
{
    [Required]
    public required string Name { get; set; }
    public string? Key { get; set; }
    public string? Description { get; set; }
    [Required]
    public required double Price { get; set; }
    [Required]
    public required int UnitInStock { get; set; }
    [Required]
    public required int Discount { get; set; }

    [Required]
    public required Guid Publisher { get; set; }
    [Required]
    public required IEnumerable<Guid> Genres { get; set; }
    [Required]
    public required IEnumerable<Guid> Platforms { get; set; }
}