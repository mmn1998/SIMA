using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Contracts;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Customers.ValueObjects;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Infrastructure.Data;
using SIMA.Persistance.Persistence;

namespace SIMA.Persistance.Repositories.Features.BranchManagement.Customers;

public class CustomerRepository : Repository<Customer>, ICustomerRepository
{
    private readonly SIMADBContext _context;

    public CustomerRepository(SIMADBContext context) : base(context)
    {
        _context = context;
    }

    public async Task<Customer> GetById(CustomerId Id)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(x => x.Id == Id);
        entity.NullCheck();
        return entity ?? throw SimaResultException.NotFound;
    }

    public async Task<Customer> GetByCode(string code)
    {
        var entity = await _context.Customers.FirstOrDefaultAsync(x => x.CustomerNumber == code);
        return entity;
    }
}