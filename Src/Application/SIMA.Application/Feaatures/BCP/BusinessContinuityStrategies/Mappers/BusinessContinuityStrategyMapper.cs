using AutoMapper;
using SIMA.Application.Contract.Features.BCP.BusinessContinuityStrategies;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Args;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.BusinessContinuityStrategies.Mappers;

public class BusinessContinuityStrategyMapper : Profile
{
    public BusinessContinuityStrategyMapper()
    {
        CreateMap<CreateBusinessContinuityStrategyCommand, CreateBusinessContinuityStategyArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.IssueId, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.ExpireDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ExpireDate)))
            .ForMember(dest => dest.ReviewDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ReviewDate)))
            ;
        CreateMap<CreateBusinessContinuityStategyArg, CreateBusinessContinuityStrategyIssueArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.BusinessContinuityStategyId, act => act.MapFrom(source => source.Id))
            ;
        CreateMap<string, CreateBusinessContinuityStratgySolutionArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.Code, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.Title, act => act.MapFrom(source => source))
            ;

        CreateMap<string, CreateBusinessContinuityStrategyObjectiveArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.Code, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.Title, act => act.MapFrom(source => source))
            ;
        CreateMap<long, CreateBusinessContinuityStrategyDocumentArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.DocumentId, act => act.MapFrom(source => source))
            ;
        CreateMap<CreateBusinessContinuityStrategyResponsibleCommand, CreateBusinessContinuityStratgyResponsibleArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyBusinessContinuityStrategyCommand, ModifyBusinessContinuityStategyArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            .ForMember(dest => dest.ExpireDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.ExpireDate)))
            
            ;
    }
}