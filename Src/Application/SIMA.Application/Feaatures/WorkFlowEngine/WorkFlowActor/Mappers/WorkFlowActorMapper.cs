using AutoMapper;
using SIMA.Application.Contract.Features.WorkFlowEngine.WorkFlowActor;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Args.Modify;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.WorkFlowEngine.WorkFlowActor.Mappers
{
    public class WorkFlowActorMapper : Profile
    {
        public WorkFlowActorMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateWorkFlowActorCommand, WorkFlowActorArg>()
                .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => simaIdentity.UserId))
                .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));

            CreateMap<ModifyWorkFlowActorCommand, ModifyWorkFlowActorArg>()
             .ForMember(dest => dest.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.Now.ToString())))
             .ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId));

            CreateMap<long, CreateWorkFlowActorRoleArg>()
                .ForMember(dest => dest.RoleId, act => act.MapFrom(source => source))
                .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
                ;
            CreateMap<long, CreateWorkFlowActorGroupArg>()
                .ForMember(dest => dest.GroupId, act => act.MapFrom(source => source))
                .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
                ;
            CreateMap<long, CreateWorkFlowActorUserArg>()
                .ForMember(dest => dest.UserId, act => act.MapFrom(source => source))
                .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
                ;

        }
    }
}
