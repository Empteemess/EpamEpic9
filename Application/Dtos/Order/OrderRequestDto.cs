using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Application.Dtos.Order;

public class OrderRequestDto
{
    public DateTime? Date { get; set; }
    [Required]
    public required Guid CustomerId { get; set; }
    [Required]
    public required StatusEnum Status { get; set; }

}