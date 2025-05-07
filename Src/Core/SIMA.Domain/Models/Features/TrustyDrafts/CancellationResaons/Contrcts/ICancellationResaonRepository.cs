using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.ValueObjects;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Contrcts;

public interface ICancellationResaonRepository : IRepository<CancellationResaon>
{
    Task<CancellationResaon> GetById(CancellationResaonId id);

    Task<CancellationResaon> GetByCode(string code);
}