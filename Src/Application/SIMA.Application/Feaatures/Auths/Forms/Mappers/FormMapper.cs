using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Forms;
using SIMA.Domain.Models.Features.Auths.Forms.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.Forms.Mappers;

public class FormMapper : Profile
{
    public FormMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateFormCommand, CreateFormArg>()
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active));
        
        CreateMap<long, CreateFormPermissionArg>()
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(x => x.ActiveFrom, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(x => x.ActiveTo, opt => opt.MapFrom(src => DateTime.Now.AddYears(1)))
            .ForMember(x => x.PermissionId, opt => opt.MapFrom(src => src))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()))
            .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active));

        CreateMap<ModifyFormCommand, ModifyFormArg>()
            .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTimeOffset.UtcNow.ToString())));
            //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId));
    }
}
