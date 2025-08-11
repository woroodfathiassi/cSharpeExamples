using DI.Services;

namespace DI.Controllers;

public class OrderController
{
    private readonly OrderService _orderService = new ();
}
