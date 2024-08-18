using AutoMapper;
using SIMA.Application.Contract.Features.Auths.ResponsibleTypes;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.ResponsibleTypes.Mappers;

public class ResponsibleTypeMapper : Profile
{
    public ResponsibleTypeMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateResponsibleTypeCommand, CreateResponsibleTypeArg>()
            .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
            .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
            .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));

        CreateMap<ModifyResponsibleTypeCommands, ModifyResponsibleTypeArg>()
            .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString())));
    }
}
