using AutoMapper;
using SIMA.Application.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.WorkFlowEngine.Progress.Mapper
{
    public class ProgressMapper : Profile
    {
        public ProgressMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<ChangeStatusCommand, ChangeStatusArg>()
             .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString())))
             .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
             .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));
        }
    }
}
