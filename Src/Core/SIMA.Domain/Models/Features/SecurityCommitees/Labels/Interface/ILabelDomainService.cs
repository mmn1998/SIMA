using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Labels.Interface;

public interface ILabelDomainService : IDomainService
{
    Task<Label> CheckExist(string label);
}
