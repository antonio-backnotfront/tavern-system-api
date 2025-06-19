using System.Net;
using Microsoft.AspNetCore.Mvc;
using TavernSystem_Training_1.Application;
using TavernSystem_Training_1.Application.Exceptions;
using TavernSystem_Training_1.Models;
using TavernSystem_Training_1.Models.DTO;

namespace TavernSystem_Training_1.API.Controllers;

[ApiController]
[Route("api/adventurers")]
public class TavernController : ControllerBase
{
    ILogger<TavernController> _logger;
    ITavernService _service;
    public TavernController(ILogger<TavernController> logger, ITavernService tavernService)
    {
        _service = tavernService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAdventurersAsync([FromQuery] string? orderBy, CancellationToken cancellationToken)
    {
        List<GetAdventurersDto> adventurers;
        if (orderBy != null)
        {
            if (orderBy.Equals("nickname", StringComparison.OrdinalIgnoreCase)) adventurers = await _service.GetAllAdventurersOrderByNicknameAsync(cancellationToken);
            else if (orderBy.Equals("raceId", StringComparison.OrdinalIgnoreCase)) adventurers = await _service.GetAllAdventurersOrderByRaceIdAsync(cancellationToken);
            else adventurers = await _service.GetAllAdventurersAsync(cancellationToken);
        } else adventurers = await _service.GetAllAdventurersAsync(cancellationToken);
        return Ok(adventurers);
    }
    
    [HttpGet("{id}", Name = "GetAdventurerById")]
    // [Route()]
    public async Task<IActionResult> GetAdventurerByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var foundAdventurer = await _service.GetAdventurerByIdAsync(id, cancellationToken);
            return foundAdventurer != null ? Ok(foundAdventurer) : NotFound($"Adventurer with id '{id}' not found.");
        }
        catch (NotExistsException e)
        {
            return BadRequest(e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem();
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateAdventurerAsync(CreateAdventurerDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var createdAdventurer = await _service.CreateAdventurerAsync(dto, cancellationToken);
            return createdAdventurer != null ? CreatedAtAction("GetAdventurerById", new {id = createdAdventurer.Id}, createdAdventurer) : Problem();
        }
        catch (InvalidPersonDataException e)
        {
            return BadRequest(e.Message);
        }
        catch (NotExistsException e)
        {
            return BadRequest(e.Message);
        }
        catch (AlreadyExistsException e)
        {
            return BadRequest(e.Message);
        }
        catch (HasBountyException e)
        {
            return StatusCode(403,e.Message);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return Problem();
        }
    }
    
}