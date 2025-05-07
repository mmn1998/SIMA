using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Departments;
using SIMA.Domain.Models.Features.Auths.Departments.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.Departments.Mappers;

public class DepartmentMapper : Profile
{
    public DepartmentMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateDepartmentCommand, CreateDepartmentArg>()
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId())); ;

        CreateMap<ModifyDepartmentCommand, ModifyDepartmentArg>()
           .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.UtcNow.ToString())))
           //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
           ;
    }
}
