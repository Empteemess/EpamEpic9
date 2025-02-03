namespace Domain.Entities;

public class Genre
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid? ParentGenreId { get; set; }

    public ICollection<GameGenre>? GameGenres { get; set; }
}