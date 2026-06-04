using Infrastructure.DTOs.Clients;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController : BaseController
{
    private readonly IClientService _client;
    public ClientsController(IClientService client)
    {
        _client = client;
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync(CreateClientDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _client.CreateAsync(dto);

        return result.IsSuccess? Ok(result.Data) : HandleError(result);
    }

    [HttpGet]
public async Task<IActionResult> GetAllAsync([FromQuery] GetClientFilterDto dto)
{
    return Ok(await _client.GetAllAsync(dto));
}

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(Guid id)
    {
        var result = await _client.GetByIdAsync(id);

        return result.IsSuccess ? Ok(result.Data): HandleError(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(Guid id, UpdateClientDto dto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _client.UpdateAsync(id, dto);

        return result.IsSuccess? Ok(result.Data): HandleError(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(Guid id)
    {
        var result = await _client.DeleteAsync(id);

        return result.IsSuccess ? Ok(result.Data) : HandleError(result);
    }
}