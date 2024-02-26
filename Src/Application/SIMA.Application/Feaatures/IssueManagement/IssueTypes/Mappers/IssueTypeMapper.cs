using AutoMapper;
using SIMA.Application.Contract.Features.IssueManagement.IssueTypes;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.IssueManagement.IssueTypes.Mappers;

public class IssueTypeMapper : Profile
{
    public IssueTypeMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateIssueTypeCommand, CreateIssueTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyIssueTypeCommand, ModifyIssueTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}
