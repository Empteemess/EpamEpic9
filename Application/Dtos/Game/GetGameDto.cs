using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Game;

public class GetGameDto
{
    public Guid Id { get; set; }
    [Required]
    public required string Name { get; set; }
    [Required]
    public required string Key { get; set; }
    public string? Description { get; set; }
    [Required]
    public required double Price { get; set; }
    [Required]
    public required int UnitInStock { get; set; }
    [Required]
    public required int Discount { get; set; }

    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
}