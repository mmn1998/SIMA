using AutoMapper;
using SIMA.Application.Contract.Features.WorkFlowEngine.Project;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Create;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Args.Modify;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.WorkFlowEngine.Project.Mapper
{
    public class ProjectMapper : Profile
    {
        public ProjectMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateProjectCommand, CreateProjectArg>()
               .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
               //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
               .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));


            CreateMap<ProjectMemberListCommand, CreateProjectMemberArg>()
               .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
               //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
               .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));


            CreateMap<long, CreateProjectGroupArg>()
                .ForMember(x => x.GroupId, opt => opt.MapFrom(src => src))
               .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
               //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
               .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => ActiveStatusEnum.Active));


            CreateMap<ModifyProjectCommand, ModifyProjectArg>()
               .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString())));
                //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId));
        }
    }
}
