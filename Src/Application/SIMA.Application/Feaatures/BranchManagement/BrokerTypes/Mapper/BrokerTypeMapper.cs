using AutoMapper;
using SIMA.Application.Contract.Features.BranchManagement.BrokerTypes;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.BranchManagement.BrokerTypes.Mapper
{
    public class BrokerTypeMapper : Profile
    {
        public BrokerTypeMapper(ISimaIdentity simaIdentity)
        {
            CreateMap<CreateBrokerTypeCommand, CreateBrokerTypeArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<ModifyBrokerTypeCommand, ModifyBrokerTypeArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                //.ForMember(dest => dest.ModifiedBy, act => act.MapFrom(source => simaIdentity.UserId))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        }
    }
}
