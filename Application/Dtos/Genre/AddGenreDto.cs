namespace Application.Dtos.Genre;

public class AddGenreDto
{
    public required string Name { get; set; }
    public Guid? ParentGenreId { get; set; }
}