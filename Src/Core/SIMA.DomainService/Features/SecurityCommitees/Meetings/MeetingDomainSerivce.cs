using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.Meetings;

namespace SIMA.DomainService.Features.SecurityCommitees.Meetings
{
    public class MeetingDomainSerivce : IMeetingDomainService
    {
        private readonly IMeetingQueryRepository _meetingQueryRepository;
        private readonly ISimaIdentity _simaIdentity;

        public MeetingDomainSerivce(IMeetingQueryRepository meetingQueryRepository, ISimaIdentity simaIdentity)
        {
            _meetingQueryRepository = meetingQueryRepository;
            _simaIdentity = simaIdentity;
        }

        public async Task<List<CreateLabelArg>> GetLabels(List<string> label)
        {
            var lableEntities = await _meetingQueryRepository.GetLabelByCode(label);
            var result =  lableEntities.Select(x => new CreateLabelArg { Code = x.Code, Id = x.Id, IsNew = false, CreatedBy = _simaIdentity.UserId }).ToList();
            var notExist = label.Where(p => !lableEntities.Any(p2 => p2.Code.Trim() == p.Trim())).Select(x => new CreateLabelArg { Code = x, Id = IdHelper.GenerateUniqueId(), IsNew = true, CreatedBy = _simaIdentity.UserId }).ToList();
            result.AddRange(notExist);
            return result;
        }


    }
}
