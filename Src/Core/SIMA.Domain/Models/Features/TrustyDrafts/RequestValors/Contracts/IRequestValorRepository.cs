using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Contracts;

public interface IRequestValorRepository : IRepository<RequestValor>
{
    Task<RequestValor> GetById(RequestValorId id);
    Task<RequestValor> GetByCode(string code);
}