namespace Application.Dtos.Order;

public class GetOrderDto
{
    public required Guid Id { get; set; }
    public Guid CustomerId { get; set; }
    public DateTime? Date { get; set; }
}