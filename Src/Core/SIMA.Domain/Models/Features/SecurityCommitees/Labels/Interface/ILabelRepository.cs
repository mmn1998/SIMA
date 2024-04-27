using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Labels.Interface;

public interface ILabelRepository : IRepository<Label>
{
    Task<Label> GetById(long Id);
}
