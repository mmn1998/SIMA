using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Args;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Interfaces;
using SIMA.Framework.Common.Helper;
using SIMA.Persistance.Read.Repositories.Features.SecurityCommitees.Meetings;

namespace SIMA.DomainService.Features.SecurityCommitees.Meetings
{
    public class MeetingDomainSerivce : IMeetingDomainService
    {
        private readonly IMeetingQueryRepository _meetingQueryRepository;

        public MeetingDomainSerivce(IMeetingQueryRepository meetingQueryRepository)
        {
            _meetingQueryRepository = meetingQueryRepository;
        }

        public async Task<List<CreateLabelArg>> GetLabels(List<string> label)
        {
            var lableEntities  = await _meetingQueryRepository.GetLabelByCode(label);
            var result  = lableEntities.Select(x => new CreateLabelArg { Code = x.Code, Id = x.Id, IsNew = false }).ToList();
            var notExist = label.Where(x=> !lableEntities.Any(s=>s.Code == x)).Select(x=>new CreateLabelArg { Code = x, Id = IdHelper.GenerateUniqueId(), IsNew = true }).ToList();
            result.AddRange(notExist);   
            return result;
        }

        
    }
}
