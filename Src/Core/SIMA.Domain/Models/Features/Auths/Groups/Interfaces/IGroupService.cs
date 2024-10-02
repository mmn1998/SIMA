using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.Auths.Groups.Interfaces;

public interface IGroupService : IDomainService
{
    Task<bool> IsCodeUnique(string code, long id);
    Task<List<long>> GetGroupPermissonByFormId(long formId , long groupId);

}
