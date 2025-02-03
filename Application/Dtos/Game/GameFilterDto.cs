using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Dtos.Game;

public class GameFilterDto
{
    [Range(3,50)]
    public string? Name { get; set; }
    public SortingEnum? SortingEnum { get; set; }
    public PaginationEnum? PaginationEnum { get; set; }
    public int? PageNumber { get; set; }
}