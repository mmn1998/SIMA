using AutoMapper;
using SIMA.Application.Contract.Features.BranchManagement.LoanTypes;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BranchManagement.LoanTypes.Mappers;

public class LoanTypeMapper : Profile
{
    public LoanTypeMapper()
    {
        CreateMap<CreateLoanTypeCommand, CreateLoanTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyLoanTypeCommand, ModifyLoanTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}