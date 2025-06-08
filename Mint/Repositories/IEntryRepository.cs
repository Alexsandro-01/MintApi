using Mint.Models;

namespace Mint.Repositories;

public interface IEntryRepository
{
  Entry AddEntry(Entry entry);
  IEnumerable<Entry> GetAllEntriesByUserId(int userId);
  Entry GetEntryByUserId(int userId, int entryId);
  IEnumerable<Entry> GetAllEntriesByUserIdAndDateRange(int userId, DateTime startDate, DateTime endDate);
  public IEnumerable<Entry> GetFilteredEntries(
  int userId,
  int? productId = null,
  int? unitId = null,
  int? customerId = null,
  DateTime? startDate = null,
  DateTime? endDate = null
  );
}