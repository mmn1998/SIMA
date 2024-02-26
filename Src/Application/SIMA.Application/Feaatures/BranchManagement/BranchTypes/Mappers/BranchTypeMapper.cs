using AutoMapper;
using SIMA.Application.Contract.Features.BranchManagement.BranchTypes;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.BranchManagement.BranchTypes.Mappers;

public class BranchTypeMapper : Profile
{
    public BranchTypeMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateBranchTypeCommand, CreateBranchTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<ModifyBranchTypeCommand, ModifyBranchTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifyBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.ModifyAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}
