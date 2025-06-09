using Microsoft.EntityFrameworkCore;
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
    return _context.Entries
    .Include(e => e.Product)
    .Include(e => e.Unit)
    .Include(e => e.Customer)
    .Where(e => e.UserId == userId)
    .Select(e => new Entry
    {
      Id = e.Id,
      UserId = e.UserId,
      ProductId = e.ProductId,
      UnitId = e.UnitId,
      CustomerId = e.CustomerId,
      SaleDate = e.SaleDate,
      Quantity = e.Quantity,
      Total = e.Total,
      Product = new Product
      {
        Id = e.Product.Id,
        UserId = e.Product.UserId,
        Name = e.Product.Name,
        Code = e.Product.Code,
        Active = e.Product.Active,
      },
      Unit = new Unit
      {
        Id = e.Unit.Id,
        UserId = e.Unit.UserId,
        Name = e.Unit.Name,
        Description = e.Unit.Description
      },
      Customer = new Customer
      {
        Id = e.Customer.Id,
        UserId = e.Customer.UserId,
        Name = e.Customer.Name
      }
    })
    .ToArray();
  }

  public Entry GetEntryByUserId(int userId, int entryId)
  {

    var result = _context.Entries
    .Include(e => e.Product)
    .Include(e => e.Unit)
    .Include(e => e.Customer)
    .Select(e => new Entry
    {
      Id = e.Id,
      UserId = e.UserId,
      ProductId = e.ProductId,
      UnitId = e.UnitId,
      CustomerId = e.CustomerId,
      SaleDate = e.SaleDate,
      Quantity = e.Quantity,
      Total = e.Total,
      Product = new Product
      {
        Id = e.Product.Id,
        UserId = e.Product.UserId,
        Name = e.Product.Name,
        Code = e.Product.Code,
        Active = e.Product.Active,
      },
      Unit = new Unit
      {
        Id = e.Unit.Id,
        UserId = e.Unit.UserId,
        Name = e.Unit.Name,
        Description = e.Unit.Description
      },
      Customer = new Customer
      {
        Id = e.Customer.Id,
        UserId = e.Customer.UserId,
        Name = e.Customer.Name
      }
    })
    .FirstOrDefault(e => e.UserId == userId && e.Id == entryId); ;

    return result;
  }

  public IEnumerable<Entry> GetAllEntriesByUserIdAndDateRange(int userId, DateTime startDate, DateTime endDate)
  {
    return _context.Entries
        .Where(e => e.UserId == userId && e.SaleDate >= startDate && e.SaleDate <= endDate)
        .ToArray();
  }

  public IEnumerable<Entry> GetFilteredEntries(
    int userId,
    int? productId = null,
    int? unitId = null,
    int? customerId = null,
    DateTime? startDate = null,
    DateTime? endDate = null
    )
  {
    var query = _context.Entries.AsQueryable();

    query.Where(e => e.UserId == userId);

    if (startDate.HasValue)
    {
      query = query.Where(e => e.SaleDate >= startDate.Value);
    }

    if (productId.HasValue)
    {
      query = query.Where(e => e.ProductId == productId.Value);
    }

    if (unitId.HasValue)
    {
      query = query.Where(e => e.UnitId == unitId.Value);
    }

    if (customerId.HasValue)
    {
      query = query.Where(e => e.CustomerId == customerId.Value);
    }

    return query.ToArray();
  }
}