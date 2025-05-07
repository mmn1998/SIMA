using AutoMapper;
using SIMA.Application.Contract.Features.BranchManagement.Customers;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BranchManagement.Customers.Mappers;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<CreateCustomerCommand, CreateCustomerArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyCustomerCommand, ModifyCustomerArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}