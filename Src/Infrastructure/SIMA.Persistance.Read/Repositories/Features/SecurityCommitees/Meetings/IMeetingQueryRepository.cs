using SIMA.Application.Query.Contract.Features.SecurityCommitees.Labels;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.Meetings
{
    public interface IMeetingQueryRepository : IQueryRepository
    {
        Task<List<GetLabelResult>> GetLabelByCode(List<string> labels);
    }
}
