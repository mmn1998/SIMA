using AutoMapper;
using SIMA.Application.Contract.Features.DMS.DocumentExtensions;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.DMS.DocumentTypes.Mappers;

internal class DocumentTypesMapper : Profile
{
    public DocumentTypesMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateDocumentExtensionCommand, CreateDocumentTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));
        CreateMap<ModifyDocumentExtensionCommand, ModifyDocumentTypeArg>()
            //.ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}
