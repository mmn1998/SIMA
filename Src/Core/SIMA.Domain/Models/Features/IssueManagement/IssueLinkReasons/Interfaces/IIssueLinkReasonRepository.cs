using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Interfaces;

public interface IIssueLinkReasonRepository : IRepository<IssueLinkReason>
{
    Task<IssueLinkReason> GetById(long id);

}
