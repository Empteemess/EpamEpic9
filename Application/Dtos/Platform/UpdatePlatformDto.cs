namespace Application.Dtos.Platform;

public class UpdatePlatformDto
{
    public Guid Id { get; set; }
    public required string Type { get; set; }
}