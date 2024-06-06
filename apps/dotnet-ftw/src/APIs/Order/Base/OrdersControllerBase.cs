using DotnetFtw.APIs;
using DotnetFtw.APIs.Dtos;
using DotnetFtw.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class OrdersControllerBase : ControllerBase
{
    protected readonly IOrdersService _service;

    public OrdersControllerBase(IOrdersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Order
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<OrderDto>> CreateOrder(OrderCreateInput input)
    {
        var order = await _service.CreateOrder(input);

        return CreatedAtAction(nameof(Order), new { id = order.Id }, order);
    }

    /// <summary>
    /// Delete one Order
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteOrder([FromRoute()] OrderIdDto idDto)
    {
        try
        {
            await _service.DeleteOrder(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Orders
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<OrderDto>>> Orders([FromQuery()] OrderFindMany filter)
    {
        return Ok(await _service.Orders(filter));
    }

    /// <summary>
    /// Get one Order
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<OrderDto>> Order([FromRoute()] OrderIdDto idDto)
    {
        try
        {
            return await _service.Order(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple Items records to Order
    /// </summary>
    [HttpPost("{Id}/items")]
    public async Task<ActionResult> ConnectItems(
        [FromRoute()] OrderIdDto idDto,
        [FromQuery()] ItemIdDto[] itemsId
    )
    {
        try
        {
            await _service.ConnectItems(idDto, itemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Items records from Order
    /// </summary>
    [HttpDelete("{Id}/items")]
    public async Task<ActionResult> DisconnectItems(
        [FromRoute()] OrderIdDto idDto,
        [FromBody()] ItemIdDto[] itemsId
    )
    {
        try
        {
            await _service.DisconnectItems(idDto, itemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Items records for Order
    /// </summary>
    [HttpGet("{Id}/items")]
    public async Task<ActionResult<List<ItemDto>>> FindItems(
        [FromRoute()] OrderIdDto idDto,
        [FromQuery()] ItemFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindItems(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Get a Customer record for Order
    /// </summary>
    [HttpGet("{Id}/customers")]
    public async Task<ActionResult<List<CustomerDto>>> GetCustomer([FromRoute()] OrderIdDto idDto)
    {
        var customer = await _service.GetCustomer(idDto);
        return Ok(customer);
    }

    /// <summary>
    /// Update multiple Items records for Order
    /// </summary>
    [HttpPatch("{Id}/items")]
    public async Task<ActionResult> UpdateItems(
        [FromRoute()] OrderIdDto idDto,
        [FromBody()] ItemIdDto[] itemsId
    )
    {
        try
        {
            await _service.UpdateItems(idDto, itemsId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Update one Order
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateOrder(
        [FromRoute()] OrderIdDto idDto,
        [FromQuery()] OrderUpdateInput orderUpdateDto
    )
    {
        try
        {
            await _service.UpdateOrder(idDto, orderUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
