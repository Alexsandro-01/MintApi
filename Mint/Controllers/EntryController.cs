
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mint.Constants;
using Mint.Dto;
using Mint.DTO;
using Mint.Services;

namespace Mint.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
[Authorize(Policy = "User")]
public class EntryController : Controller
{
  private readonly IEntryService _entryService;
  private readonly IUserService _userService;

  public EntryController(IEntryService entryService, IUserService userService)
  {
    _entryService = entryService;
    _userService = userService;
  }

  [HttpPost]
  public ActionResult<EntryDtoResponse> AddEntry([FromBody] EntryDtoInsert entry)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var email = token.FindFirst(ClaimTypes.Email)?.Value;

    var user = _userService.GetUserByEmail(email);
    int userId = user.Id;

    entry.UserId = userId;

    EntryDtoResponse newEntry = _entryService.AddEntry(userId, entry);

    if (!newEntry.Success)
    {
      return BadRequest(newEntry);
    }

    return CreatedAtAction(nameof(GetEntryByUserId), new { entryId = newEntry.Entry[0].Id }, newEntry);
  }

  [HttpGet("{entryId}")]
  public ActionResult<EntryDtoResponse> GetEntryByUserId(int entryId)
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var email = token.FindFirst(ClaimTypes.Email)?.Value;
    var user = _userService.GetUserByEmail(email);
    int userId = user.Id;

    EntryDtoResponse entry = _entryService.GetEntryByUserId(userId, entryId);
    if (entry == null)
    {
      return NotFound(new EntryDtoResponse
      {
        Success = false,
        Message = ErrorMessages.EntryNotFound,
        Entry = null
      });
    }

    return Ok(entry);
  }

  [HttpGet]
  public ActionResult<IEnumerable<EntryDtoResponse>> GetAllEntriesByUser()
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var email = token.FindFirst(ClaimTypes.Email)?.Value;

    var user = _userService.GetUserByEmail(email);
    int userId = user.Id;

    EntryDtoResponse entries = _entryService.GetAllEntriesByUserId(userId);

    return Ok(
      entries
    );
  }

  [HttpGet("filter")]
  public ActionResult<IEnumerable<EntryDtoResponse>> GetFilteredEntries(
    [FromQuery] int? productId = null,
    [FromQuery] int? unitId = null,
    [FromQuery] int? customerId = null,
    [FromQuery] DateTime? startDate = null,
    [FromQuery] DateTime? endDate = null
  )
  {
    var token = HttpContext.User.Identity as ClaimsIdentity;
    var email = token.FindFirst(ClaimTypes.Email)?.Value;
    var user = _userService.GetUserByEmail(email);
    int userId = user.Id;

    EntryDtoResponse entries = _entryService.GetEntryFiltered(
      userId,
      productId,
      unitId,
      customerId,
      startDate,
      endDate
    );

    if (entries == null || entries.Entry == null || !entries.Entry.Any())
    {
      return NotFound(new EntryDtoResponse
      {
        Success = false,
        Message = ErrorMessages.EntryNotFound,
        Entry = null
      });
    }
    return Ok(entries);
  }
}