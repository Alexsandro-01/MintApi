using Mint.Constants;
using Mint.DTO;
using Mint.Models;
using Mint.Repositories;

namespace Mint.Services;

public class EntryService : IEntryService
{
  private readonly IEntryRepository _entryRepository;
  private readonly IProductService _productService;
  private readonly IUnitService _unitService;
  private readonly ICustomerService _customerService;

  public EntryService(
    IEntryRepository entryRepository
    , IProductService productService
    , IUnitService unitService
    , ICustomerService customerService
  )
  {
    _entryRepository = entryRepository;
    _productService = productService;
    _unitService = unitService;
    _customerService = customerService;
  }

  public EntryDtoResponse AddEntry(int userId, EntryDtoInsert entryDto)
  {
    var errors = entryDto.Validate();

    // Validate Product
    var product = _productService.GetProductByUserId(userId, entryDto.ProductId);
    if (product == null)
    {
      errors.Add(ErrorMessages.ProductNotFound);
    }

    // Validate Unit
    var unit = _unitService.GetUnitByUserId(userId, entryDto.UnitId);
    if (unit == null)
    {
      errors.Add(ErrorMessages.UnityNotFound);
    }

    // Validate Customer
    var customer = _customerService.GetCustomerByUserId(userId, entryDto.CustomerId);
    if (customer == null)
    {
      errors.Add(ErrorMessages.CustomerNotFound);
    }

    if (errors.Count > 0)
    {
      return new EntryDtoResponse
      {
        Success = false,
        Message = string.Join(", ", errors),
        Entry = null
      };
    }

    var entry = new Entry
    {
      UserId = userId,
      ProductId = entryDto.ProductId,
      UnitId = entryDto.UnitId,
      CustomerId = entryDto.CustomerId,
      Quantity = entryDto.Quantity,
      Cost = entryDto.Cost,
      Total = (int)(entryDto.Quantity * entryDto.Cost),
      SaleDate = entryDto.SaleDate,
      CreatedAt = DateTime.UtcNow
    };

    var addedEntry = _entryRepository.AddEntry(entry);

    return new EntryDtoResponse
    {
      Success = true,
      Message = "Entry added successfully",
      Entry = new[] { new EntryDto
            {
                Id = addedEntry.Id,
                UserId = addedEntry.UserId,
                ProductId = addedEntry.ProductId,
                UnitId = addedEntry.UnitId,
                CustomerId = addedEntry.CustomerId,
                Quantity = addedEntry.Quantity,
                Cost = addedEntry.Cost,
                Total = (int)(addedEntry.Quantity * addedEntry.Cost),
                SaleDate = addedEntry.SaleDate,
                CreatedAt = addedEntry.CreatedAt
            } }
    };
  }

  public EntryDtoResponse GetAllEntriesByUserId(int userId)
  {
    var entries = _entryRepository.GetAllEntriesByUserId(userId);
    return new EntryDtoResponse
    {
      Success = true,
      Message = "Entries retrieved successfully",
      Entry = entries.Select(e => new EntryDto
      {
        Id = e.Id,
        UserId = e.UserId,
        ProductId = e.ProductId,
        Product = e.Product,
        UnitId = e.UnitId,
        Unit = e.Unit,
        CustomerId = e.CustomerId,
        Customer = e.Customer,
        Quantity = e.Quantity,
        Cost = e.Cost,
        Total = (int)(e.Quantity * e.Cost),
        SaleDate = e.SaleDate,
        CreatedAt = e.CreatedAt
      }).ToArray()
    };
  }

  public EntryDtoResponse GetEntryByUserId(int userId, int entryId)
  {
    var entry = _entryRepository.GetEntryByUserId(userId, entryId);
    if (entry == null)
    {
      return new EntryDtoResponse
      {
        Success = false,
        Message = ErrorMessages.EntryNotFound,
        Entry = null
      };
    }

    return new EntryDtoResponse
    {
      Success = true,
      Message = "Entry retrieved successfully",
      Entry = new[]
      {
        new EntryDto
        {
          Id = entry.Id,
          UserId = entry.UserId,
          ProductId = entry.ProductId,
          UnitId = entry.UnitId,
          CustomerId = entry.CustomerId,
          Quantity = entry.Quantity,
          Cost = entry.Cost,
          Total = entry.Total,
          SaleDate = entry.SaleDate,
          CreatedAt = entry.CreatedAt
        }
      }
    };
  }

  public EntryDtoResponse GetAllEntriesByUserIdAndDateRange(int userId, DateTime startDate, DateTime endDate)
  {
    var entries = _entryRepository.GetAllEntriesByUserIdAndDateRange(userId, startDate, endDate);
    if (entries == null || !entries.Any())
    {
      return new EntryDtoResponse
      {
        Success = false,
        Message = ErrorMessages.EntryNotFoundInDateRange,
        Entry = null
      };
    }

    return new EntryDtoResponse
    {
      Success = true,
      Message = SuccesMessages.EntryRetrievedByDateRange,
      Entry = entries.Select(e => new EntryDto
      {
        Id = e.Id,
        UserId = e.UserId,
        ProductId = e.ProductId,
        UnitId = e.UnitId,
        CustomerId = e.CustomerId,
        Quantity = e.Quantity,
        Cost = e.Cost,
        Total = (int)(e.Quantity * e.Cost),
        SaleDate = e.SaleDate,
        CreatedAt = e.CreatedAt
      }).ToArray()
    };
  }

  public EntryDtoResponse GetEntryFiltered(
    int userId,
    int? productId = null,
    int? unitId = null,
    int? customerId = null,
    DateTime? startDate = null,
    DateTime? endDate = null
  )
  {
    var entries = _entryRepository.GetFilteredEntries(
      userId,
      productId,
      unitId,
      customerId,
      startDate,
      endDate
    );

    if (entries == null || !entries.Any())
    {
      return new EntryDtoResponse
      {
        Success = false,
        Message = ErrorMessages.EntryNotFound,
        Entry = null
      };
    }

    return new EntryDtoResponse
    {
      Success = true,
      Message = SuccesMessages.EntryFound,
      Entry = entries.Select(e => new EntryDto
      {
        Id = e.Id,
        UserId = e.UserId,
        ProductId = e.ProductId,
        UnitId = e.UnitId,
        CustomerId = e.CustomerId,
        Quantity = e.Quantity,
        Cost = e.Cost,
        Total = (int)(e.Quantity * e.Cost),
        SaleDate = e.SaleDate,
        CreatedAt = e.CreatedAt
      }).ToArray()
    };
  }



}
