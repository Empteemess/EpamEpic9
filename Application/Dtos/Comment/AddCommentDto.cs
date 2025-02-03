using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.Comment;

public class AddCommentDto
{
    public CommentDto Comment { get; set; }
    public Guid? ParentId { get; set; }
    public string? Action { get; set; }
}