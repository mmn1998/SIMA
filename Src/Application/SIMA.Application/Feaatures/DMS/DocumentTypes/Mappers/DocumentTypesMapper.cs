using AutoMapper;
using SIMA.Application.Contract.Features.DMS.DocumentExtensions;
using SIMA.Application.Contract.Features.DMS.DocumentTypes;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.DMS.DocumentTypes.Mappers;

internal class DocumentTypesMapper : Profile
{
    public DocumentTypesMapper()
    {
        CreateMap<CreateDocumentTypeCommand, CreateDocumentTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));
        CreateMap<ModifyDocumentTypeCommand, ModifyDocumentTypeArg>()
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
    }
}
