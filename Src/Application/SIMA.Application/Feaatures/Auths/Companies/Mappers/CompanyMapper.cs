using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Companies;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Args;
using SIMA.Domain.Models.Features.Auths.Companies.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.Companies.Mappers
{
    public class CompanyMapper : Profile
    {
        public CompanyMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateCompanyCommand, CreateCompanyArg>()
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
                .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));

            CreateMap<ModifyCompanyCommands, ModifyCompanyArg>()
                .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString())))
                //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))

                ;
        }
    }
}
