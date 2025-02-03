using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class Order
{
    [Required]
    public required Guid Id { get; set; }
    public DateTime? Date { get; set; }
    [Required]
    public required Guid CustomerId { get; set; }
    [Required]
    public required StatusEnum Status { get; set; }

    public ICollection<OrderGame>? OrderGames { get; set; }
}