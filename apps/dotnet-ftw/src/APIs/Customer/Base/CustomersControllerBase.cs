using DotnetFtw.APIs;
using DotnetFtw.APIs.Dtos;
using DotnetFtw.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class CustomersControllerBase : ControllerBase
{
    protected readonly ICustomersService _service;

    public CustomersControllerBase(ICustomersService service)
    {
        _service = service;
    }

    /// <summary>
    /// Create one Customer
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<CustomerDto>> CreateCustomer(CustomerCreateInput input)
    {
        var customer = await _service.CreateCustomer(input);

        return CreatedAtAction(nameof(Customer), new { id = customer.Id }, customer);
    }

    /// <summary>
    /// Connect multiple Orders records to Customer
    /// </summary>
    [HttpPost("{Id}/orders")]
    public async Task<ActionResult> ConnectOrders(
        [FromRoute()] CustomerIdDto idDto,
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
    /// Disconnect multiple Orders records from Customer
    /// </summary>
    [HttpDelete("{Id}/orders")]
    public async Task<ActionResult> DisconnectOrders(
        [FromRoute()] CustomerIdDto idDto,
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
    /// Find multiple Orders records for Customer
    /// </summary>
    [HttpGet("{Id}/orders")]
    public async Task<ActionResult<List<OrderDto>>> FindOrders(
        [FromRoute()] CustomerIdDto idDto,
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
    /// Get a address record for Customer
    /// </summary>
    [HttpGet("{Id}/addresses")]
    public async Task<ActionResult<List<AddressDto>>> GetAddress([FromRoute()] CustomerIdDto idDto)
    {
        var address = await _service.GetAddress(idDto);
        return Ok(address);
    }

    /// <summary>
    /// Update multiple Orders records for Customer
    /// </summary>
    [HttpPatch("{Id}/orders")]
    public async Task<ActionResult> UpdateOrders(
        [FromRoute()] CustomerIdDto idDto,
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
    /// Delete one Customer
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteCustomer([FromRoute()] CustomerIdDto idDto)
    {
        try
        {
            await _service.DeleteCustomer(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Customers
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<CustomerDto>>> Customers(
        [FromQuery()] CustomerFindMany filter
    )
    {
        return Ok(await _service.Customers(filter));
    }

    /// <summary>
    /// Get one Customer
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<CustomerDto>> Customer([FromRoute()] CustomerIdDto idDto)
    {
        try
        {
            return await _service.Customer(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Customer
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateCustomer(
        [FromRoute()] CustomerIdDto idDto,
        [FromQuery()] CustomerUpdateInput customerUpdateDto
    )
    {
        try
        {
            await _service.UpdateCustomer(idDto, customerUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
