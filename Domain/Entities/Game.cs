using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Game
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
    [Required]
    public required Guid PublisherId { get; set; }
    public Publisher? Publisher { get; set; }
    public DateTime? TimeStamp { get; set; }

    public ICollection<GameGenre>? GameGenres { get; set; }
    public ICollection<Comment>? Comments { get; set; }
    public ICollection<OrderGame>? OrderGames { get; set; }
    public ICollection<GamePlatform>? GamePlatforms { get; set; }
}