using Mint.Models;

namespace Mint.Repositories;

public class UnitRepository : IUnitRepository
{
  private readonly DataBaseContext _context;

  public UnitRepository(DataBaseContext context)
  {
    _context = context;
  }

  public Unit AddUnit(Unit unit)
  {
    _context.Units.Add(unit);
    _context.SaveChanges();

    return unit;
  }

  public IEnumerable<Unit> GetAllUnitiesByUser(int userId)
  {
    Unit[] units = _context.Units.Where(c => c.UserId == userId).ToArray();
    return units;
  }

  public Unit GetUnitByUserId(int userId, int unitId)
  {
    Unit unit = _context.Units.FirstOrDefault(
        c => c.UserId == userId && c.Id == unitId
    );

    return unit;
  }
}