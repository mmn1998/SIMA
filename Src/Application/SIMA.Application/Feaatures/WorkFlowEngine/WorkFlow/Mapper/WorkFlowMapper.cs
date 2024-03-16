using AutoMapper;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.WorkFlowTask;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.WorkFlowEngine.WorkFlow.Mapper
{
    public class WorkFlowMapper : Profile
    {
        public WorkFlowMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateWorkFlowCommand, CreateWorkFlowArg>()
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
                .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));

            CreateMap<ModifyWorkFlowCommand, ModifyWorkFlowArg>()
           .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
           .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId));
            ;


            CreateMap<CreateStepCommand, StepArg>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => simaIdentity.UserId))
                .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));



            CreateMap<ModifyStateCommand, ModifyStateArgs>()
          .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
          .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId));

            CreateMap<CreateStateCommand, CreateStateArg>()
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
                .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));

            CreateMap<ModifyStepCommand, ModifyStepArgs>()
        .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
        .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId));
        }
    }
}
