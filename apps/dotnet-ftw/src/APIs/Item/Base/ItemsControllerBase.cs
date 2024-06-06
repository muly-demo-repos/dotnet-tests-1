using DotnetFtw.APIs;
using DotnetFtw.APIs.Dtos;
using DotnetFtw.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class ItemsControllerBase : ControllerBase
{
    protected readonly IItemsService _service;

    public ItemsControllerBase(IItemsService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Item
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<ItemDto>> CreateItem(ItemCreateInput input)
    {
        var item = await _service.CreateItem(input);

        return CreatedAtAction(nameof(Item), new { id = item.Id }, item);
    }

    /// <summary>
    /// Delete one Item
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteItem([FromRoute()] ItemIdDto idDto)
    {
        try
        {
            await _service.DeleteItem(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Items
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<ItemDto>>> Items([FromQuery()] ItemFindMany filter)
    {
        return Ok(await _service.Items(filter));
    }

    /// <summary>
    /// Get one Item
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<ItemDto>> Item([FromRoute()] ItemIdDto idDto)
    {
        try
        {
            return await _service.Item(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Connect multiple Orders records to Item
    /// </summary>
    [HttpPost("{Id}/orders")]
    public async Task<ActionResult> ConnectOrders(
        [FromRoute()] ItemIdDto idDto,
        [FromQuery()] OrderIdDto[] ordersId
    )
    {
        try
        {
            await _service.ConnectOrders(idDto, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Disconnect multiple Orders records from Item
    /// </summary>
    [HttpDelete("{Id}/orders")]
    public async Task<ActionResult> DisconnectOrders(
        [FromRoute()] ItemIdDto idDto,
        [FromBody()] OrderIdDto[] ordersId
    )
    {
        try
        {
            await _service.DisconnectOrders(idDto, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find multiple Orders records for Item
    /// </summary>
    [HttpGet("{Id}/orders")]
    public async Task<ActionResult<List<OrderDto>>> FindOrders(
        [FromRoute()] ItemIdDto idDto,
        [FromQuery()] OrderFindMany filter
    )
    {
        try
        {
            return Ok(await _service.FindOrders(idDto, filter));
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update multiple Orders records for Item
    /// </summary>
    [HttpPatch("{Id}/orders")]
    public async Task<ActionResult> UpdateOrders(
        [FromRoute()] ItemIdDto idDto,
        [FromBody()] OrderIdDto[] ordersId
    )
    {
        try
        {
            await _service.UpdateOrders(idDto, ordersId);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Update one Item
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateItem(
        [FromRoute()] ItemIdDto idDto,
        [FromQuery()] ItemUpdateInput itemUpdateDto
    )
    {
        try
        {
            await _service.UpdateItem(idDto, itemUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
