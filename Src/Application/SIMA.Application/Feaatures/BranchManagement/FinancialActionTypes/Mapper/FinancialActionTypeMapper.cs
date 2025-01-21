using AutoMapper;
using SIMA.Application.Contract.Features.BranchManagement.FinancialActionTypes;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.BranchManagement.FinancialActionTypes.Mapper
{
    public class FinancialActionTypeMapper : Profile
    {
        public FinancialActionTypeMapper()
        {
            CreateMap<CreateFinancialActionTypeCommand, CreateFinancialActionTypeArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<ModifyFinancialActionTypeCommand, ModifyFinancialActionTypeArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        }
    }
}
