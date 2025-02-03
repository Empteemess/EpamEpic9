namespace Application.Dtos.Genre;

public class UpdateGenreDto
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public Guid? ParentGenreId { get; set; }
}