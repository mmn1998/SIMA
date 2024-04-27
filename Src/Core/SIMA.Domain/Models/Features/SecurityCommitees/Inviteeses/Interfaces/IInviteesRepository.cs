using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Interfaces;

public interface IInviteesRepository : IRepository<Invitees>
{
    Task<Invitees> GetById(long Id);
}
