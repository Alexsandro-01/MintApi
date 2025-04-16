using Mint.Dto;
using Mint.Models;

public interface IUnitService
{
    UnityDto[] GetAllUnitiesByUser(int userId);
    Unit GetUnitByUserId(int userId, int unitId);
    UnityDto AddUnit(int userId, UnityDtoInsert unity);
}