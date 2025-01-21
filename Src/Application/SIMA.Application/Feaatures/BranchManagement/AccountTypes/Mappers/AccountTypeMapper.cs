using AutoMapper;
using SIMA.Application.Contract.Features.BranchManagement.AccountTypes;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BranchManagement.AccountTypes.Mappers;

public class AccountTypeMapper : Profile
{
    public AccountTypeMapper()
    {
        CreateMap<CreateAccountTypeCommand, CreateAccountTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyAccountTypeCommand, ModifyAccountTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}