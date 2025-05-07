using AutoMapper;
using SIMA.Application.Contract.Features.BranchManagement.CurrencyOprationTypes;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.BranchManagement.CurrencyOprationTypes.Mapper
{
    public class CurrencyOprationTypeMapper : Profile
    {
        public CurrencyOprationTypeMapper()
        {
            CreateMap<CreateCurrencyOperationTypeCommand, CreateCurrencyOprationTypeArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<ModifyCurrencyOperationTypeCommand, ModifyCurrencyOprationTypeArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.ModifyAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        }
    }
}
