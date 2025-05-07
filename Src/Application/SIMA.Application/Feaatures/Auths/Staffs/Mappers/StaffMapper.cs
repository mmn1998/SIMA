using AutoMapper;
using SIMA.Application.Contract.Features.Auths.Staffs;
using SIMA.Domain.Models.Features.Auths.Staffs.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.Staffs.Mappers
{
    public class StaffMapper : Profile
    {
        public StaffMapper(ISimaIdentity simaIdentity)
        {

            CreateMap<CreateStaffCommand, CreateStaffArg>()
                .ForMember(x => x.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(x => x.ActiveStatusId, opt => opt.MapFrom(src => (int)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.CreatedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
                .ForMember(x => x.Id, opt => opt.MapFrom(src => IdHelper.GenerateUniqueId()));

            CreateMap<ModifyStaffCommand, ModifyStaffArg>()
            .ForMember(x => x.ModifiedAt, opt => opt.MapFrom(src => Encoding.UTF8.GetBytes(DateTime.UtcNow.ToString())))
            //.ForMember(dest => dest.ModifiedBy, opt => opt.MapFrom(src => simaIdentity.UserId))
            ;
        }
    }
}
