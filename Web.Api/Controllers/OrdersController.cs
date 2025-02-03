using Domain.IRepositories;
using Microsoft.AspNetCore.Mvc;

namespace Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }
    
    [HttpGet("orders/{id}")]
    public async Task<IActionResult> GetOrderById(Guid id)
    {
        var paymentMethods = await _orderService.GetOrderByIdAsync(id);
        return Ok(paymentMethods);
    }
    
    [HttpGet("cart")]
    public async Task<IActionResult> GetAllOrderGames()
    {
        var paymentMethods = await _orderService.AllOrderGamesAsync();
        return Ok(paymentMethods);
    }
    
    [HttpGet("payment-methods")]
    public async Task<IActionResult> GetAllPaymentMethods()
    {
        var paymentMethods = await _orderService.AllPaymentMethodsAsync();
        return Ok(paymentMethods);
    }

    [HttpDelete("cart/{key}")]
    public async Task<IActionResult> DeleteOrderAsync(string key)
    {
        await _orderService.DeleteGameAsync(key);
        return Ok();
    }
}