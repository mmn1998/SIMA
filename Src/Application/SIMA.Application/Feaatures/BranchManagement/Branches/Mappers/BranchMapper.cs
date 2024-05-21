using AutoMapper;
using SIMA.Application.Contract.Features.BranchManagement.Branches;
using SIMA.Domain.Models.Features.BranchManagement.Branches.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.BranchManagement.Branches.Mappers;

public class BranchMapper : Profile
{
    public BranchMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateBranchCommand, CreateBranchArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            ;
        CreateMap<ModifyBranchCommand, ModifyBranchArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            //.ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}
