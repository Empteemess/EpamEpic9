namespace Domain.Entities;

public class GamePlatform
{
    public Guid Id { get; set; }
    
    public required Guid GameId { get; set; }
    public Game Game { get; set; }

    public required Guid PlatformId { get; set; }
    public Platform Platform { get; set; }
}