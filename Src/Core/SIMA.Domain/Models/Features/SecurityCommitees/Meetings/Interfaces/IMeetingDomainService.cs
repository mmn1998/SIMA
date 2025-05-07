using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;
using SIMA.Framework.Core.Domain;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;

public interface IMeetingDomainService : IDomainService
{
    Task<List<CreateLabelArg>> GetLabels(List<string> label);
}
