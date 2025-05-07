using AutoMapper;
using SIMA.Application.Contract.Features.Auths.SupplierRanks;
using SIMA.Domain.Models.Features.Auths.SupplierRanks.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.Auths.SupplierRanks.Mappers;

public class SupplierRankMapper : Profile
{
    public SupplierRankMapper()
    {
        CreateMap<CreateSupplierRankCommand, CreateSupplierRankArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifySupplierRankCommand, ModifySupplierRankArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}