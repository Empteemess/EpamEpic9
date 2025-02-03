using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class OrderGame
{
    public Guid Id { get; set; }
    [Required]
    public required Guid OrderId { get; set; }
    public Order? Order { get; set; }

    [Required]
    public required Guid ProductId { get; set; }
    public Game? Game { get; set; }
    
    [Required]
    public required double Price { get; set; }
    [Required]
    public required int Quantity { get; set; }
    public int? Discount { get; set; }
}