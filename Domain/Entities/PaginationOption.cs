using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class PaginationOption
{
    public Guid Id { get; set; }
    public required string Option { get; set; }
}