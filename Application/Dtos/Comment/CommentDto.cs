using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Application.Dtos.Comment;

public class CommentDto
{
    [JsonIgnore]
    public Guid Id { get; set; }
        
    [Required]
    public required string Name { get; set; }
        
    [Required]
    public required string Body { get; set; }
        
    public IEnumerable<CommentDto> ChildComments { get; set; }
}