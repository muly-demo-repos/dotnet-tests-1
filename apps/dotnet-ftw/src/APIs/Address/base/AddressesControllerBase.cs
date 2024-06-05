using DotnetFtw.APIs;
using DotnetFtw.APIs.Dtos;
using DotnetFtw.APIs.Errors;
using Microsoft.AspNetCore.Mvc;

namespace DotnetFtw.APIs;

[Route("api/[controller]")]
[ApiController()]
public abstract class AddressesControllerBase : ControllerBase
{
    protected readonly IAddressesService _service;

    public AddressesControllerBase(IAddressesService service)
    {
        _service = service;
    }

    /// <summary>
    /// Get a Customer record for Address
    /// </summary>
    [HttpGet("{Id}/customers")]
    public async Task<ActionResult<List<CustomerDto>>> GetCustomer([FromRoute()] AddressIdDto idDto)
    {
        var customer = await _service.GetCustomer(idDto);
        return Ok(customer);
    }

    /// <summary>
    /// Create one Address
    /// </summary>
    [HttpPost()]
    public async Task<ActionResult<AddressDto>> CreateAddress(AddressCreateInput input)
    {
        var address = await _service.CreateAddress(input);

        return CreatedAtAction(nameof(Address), new { id = address.Id }, address);
    }

    /// <summary>
    /// Delete one Address
    /// </summary>
    [HttpDelete("{Id}")]
    public async Task<ActionResult> DeleteAddress([FromRoute()] AddressIdDto idDto)
    {
        try
        {
            await _service.DeleteAddress(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }

    /// <summary>
    /// Find many Addresses
    /// </summary>
    [HttpGet()]
    public async Task<ActionResult<List<AddressDto>>> Addresses(
        [FromQuery()] AddressFindMany filter
    )
    {
        return Ok(await _service.Addresses(filter));
    }

    /// <summary>
    /// Get one Address
    /// </summary>
    [HttpGet("{Id}")]
    public async Task<ActionResult<AddressDto>> Address([FromRoute()] AddressIdDto idDto)
    {
        try
        {
            return await _service.Address(idDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }
    }

    /// <summary>
    /// Update one Address
    /// </summary>
    [HttpPatch("{Id}")]
    public async Task<ActionResult> UpdateAddress(
        [FromRoute()] AddressIdDto idDto,
        [FromQuery()] AddressUpdateInput addressUpdateDto
    )
    {
        try
        {
            await _service.UpdateAddress(idDto, addressUpdateDto);
        }
        catch (NotFoundException)
        {
            return NotFound();
        }

        return NoContent();
    }
}
