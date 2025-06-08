using Mint.DTO;

namespace Mint.Services;

public interface IEntryService
{
  EntryDtoResponse AddEntry(int userId, EntryDtoInsert entry);
  EntryDtoResponse GetEntryByUserId(int userId, int entryId);
  EntryDtoResponse GetAllEntriesByUserId(int userId);
  EntryDtoResponse GetAllEntriesByUserIdAndDateRange(int userId, DateTime startDate, DateTime endDate);

  public EntryDtoResponse GetEntryFiltered(
    int userId,
    int? productId = null,
    int? unitId = null,
    int? customerId = null,
    DateTime? startDate = null,
    DateTime? endDate = null
  );
}