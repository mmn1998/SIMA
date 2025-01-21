using AutoMapper;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.State;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.Steps;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlow.WorkFlowTask;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Args.Modify;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
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
                .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));

            CreateMap<ModifyWorkFlowCommand, ModifyWorkFlowArg>()
           .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())));


            CreateMap<CreateStepCommand, StepArg>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active))
                .AfterMap((c, s) =>
                {
                    foreach (var item in c.ActorId)
                    {
                        CreateWorkFlowActorStepArg actorStepArg = new CreateWorkFlowActorStepArg();
                        actorStepArg.WorkFlowActorId = item;
                        actorStepArg.ActiveStatusId = (long)ActiveStatusEnum.Active;
                        actorStepArg.Id = IdHelper.GenerateUniqueId();
                        s.ActorStepArgs.Add(actorStepArg);
                    }
                });


            CreateMap<ModifyStateCommand, ModifyStateArgs>()
          .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())));

            CreateMap<CreateStateCommand, CreateStateArg>()
               .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
               .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));

            CreateMap<AddStepApprovalOption, CreateStepApprovalOptionArg>()
               .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
             .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));

            CreateMap<RequiredDocument, CreateStepRequiredDocumentArg>()
               .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active))
               .ForMember(x => x.DocumentTypeId, opt => opt.MapFrom(src => src.DocumentTypeId))
               .ForMember(x => x.Count, opt => opt.MapFrom(src => src.Count));

            CreateMap<ModifyStepCommand, ModifyStepArgs>()
        .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())));
        }
    }
}
