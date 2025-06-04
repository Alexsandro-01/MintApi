using Mint.Models;

namespace Mint.Repositories;

public class EntryRepository : IEntryRepository
{
  private readonly DataBaseContext _context;

  public EntryRepository(DataBaseContext context)
  {
    _context = context;
  }

  public Entry AddEntry(Entry entry)
  {
    _context.Entries.Add(entry);
    _context.SaveChanges();

    return entry;
  }

  public IEnumerable<Entry> GetAllEntriesByUserId(int userId)
  {
    return _context.Entries.Where(e => e.UserId == userId).ToArray();
  }

  public Entry GetEntryByUserId(int userId, int entryId)
  {
    return _context.Entries.FirstOrDefault(e => e.UserId == userId && e.Id == entryId);
  }

  public IEnumerable<Entry> GetAllEntriesByUserIdAndDateRange(int userId, DateTime startDate, DateTime endDate)
  {
    return _context.Entries
        .Where(e => e.UserId == userId && e.SaleDate >= startDate && e.SaleDate <= endDate)
        .ToArray();
  }
}