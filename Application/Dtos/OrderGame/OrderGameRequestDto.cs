using System.ComponentModel.DataAnnotations;

namespace Application.Dtos.OrderGame;

public  class OrderGameRequestDto
{
    [Required]
    public required Guid ProductId { get; set; }
    [Required]
    public required double Price { get; set; }
    [Required]
    public required int Quantity { get; set; }
    public int? Discount { get; set; }
}