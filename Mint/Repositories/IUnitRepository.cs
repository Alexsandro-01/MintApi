using Mint.Models;

namespace Mint.Repositories;

public interface IUnitRepository
{
    Unit GetUnitByUserId(int userId, int unitId);
    IEnumerable<Unit> GetAllUnitiesByUser(int userId);
    Unit AddUnit(Unit unit);
}