namespace Mint.Services
{
  using System.Collections.Generic;
  using System.Linq;
  using Mint.Dto;
  using Mint.Models;
  using Mint.Repositories;

  public class UnitService : IUnitService
  {
    private readonly UnitRepository _unitRepository;

    public UnitService(IUnitRepository unitRepository)
    {
      _unitRepository = (UnitRepository)unitRepository;
    }

    public UnityDto AddUnit(int userId, UnityDtoInsert unity)
    {
      Unit unit = new Unit()
      {
        UserId = userId,
        Name = unity.Name,
        Description = unity.Description ?? "",
        CreatedAt = DateTime.UtcNow
      };

      Unit response = _unitRepository.AddUnit(unit);

      UnityDto unityDto = new UnityDto
      {
        UnityId = unit.Id,
        Description = unit.Description,
        Name = unit.Name
      };
      
      return unityDto;
    }

    public UnityDto[] GetAllUnitiesByUser(int userId)
    {
      IEnumerable<Unit> units = _unitRepository.GetAllUnitiesByUser(userId);

      UnityDto[] unityDtos = units.Select(
      u => new UnityDto
      {
        UnityId = u.Id,
        Name = u.Name,
        Description = u.Description
      })
      .ToArray();

      return unityDtos;
    }

    public Unit GetUnitByUserId(int userId, int unitId)
    {
      Unit unit = _unitRepository.GetUnitByUserId(userId, unitId);

      return unit;
    }
  }
}