using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Comment
{
    public Guid Id { get; set; }

    [Required]
    public required string Name { get; set; }

    [Required]
    public required string Body { get; set; }

    public Guid? ParentCommentId { get; set; } 
    public Comment? ParentComment { get; set; } 

    [Required]
    public required Guid GameId { get; set; } 
    public Game Game { get; set; } 

    public ICollection<Comment> ChildComments { get; set; } = new List<Comment>(); 
}