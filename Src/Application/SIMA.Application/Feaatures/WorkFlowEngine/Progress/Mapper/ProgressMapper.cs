using AutoMapper;
using SIMA.Application.Contract.Features.WorkFlowEngine.Progress;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Args;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.WorkFlowEngine.Progress.Mapper
{
    public class ProgressMapper : Profile
    {
        public ProgressMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<ProgressStoreProcedureCommand, ProgressStoreProcedureArg>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                 .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active))
                 .ForMember(x => x.ProgressStoreProcedureParams, opt => opt.MapFrom(src => src.Params));
            CreateMap<ProgressStoreProcedureParamCommand, ProgressStoreProcedureParamArg>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
               // .ForMember(x => x.ProgressStoreProcedureId, opt => opt.MapFrom(src => src.ProgressStoreProcedureId))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                 .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));
            CreateMap<ModifyProgressCommand, ChangeStatusArg>()
             .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString())))
             .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active))
             .ForMember(x => x.ProgressStoreProcedures, opt => opt.MapFrom(src => src.StoreProcedures))             
             ;
        }
    }
}
