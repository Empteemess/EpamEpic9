using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Game;

public class UpdateGameRequestDto
{
    public Guid GameId { get; set; }
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
    public required Guid PublisherId { get; set; }
    public required IEnumerable<Guid> Genres { get; set; }
    public required IEnumerable<Guid> Platforms { get; set; }
}