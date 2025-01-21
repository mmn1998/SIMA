using AutoMapper;
using SIMA.Application.Contract.Features.BCP.BusinesImpactAnalysises;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Args;
using SIMA.Framework.Common.Helper;
using System.Text;

namespace SIMA.Application.Feaatures.BCP.BusinesImpactAnalysises.Mappers;

public class BusinessImpactAnalysisMapper : Profile
{
    public BusinessImpactAnalysisMapper()
    {
        CreateMap<CreateBusinessImpactAnalysisCommand, CreateBusinessImpactAnalysisArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.IssueId, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<CreateBusinessImpactAnalysisArg, CreateBusinessImpactAnalysisIssueArg> ()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.BusinessImpactAnalysisId, act => act.MapFrom(source => source.Id))
            ;
        CreateMap<long, CreateBusinessImpactAnalysisAssetArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.AssetId, act => act.MapFrom(source => source))
            ;
        //CreateMap<long, CreateBusinessImpactAnalysisStaffArg>()
        //    .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
        //    .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
        //    .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
        //    .ForMember(dest => dest.StaffId, act => act.MapFrom(source => source))
        //    ;
        CreateMap<long, CreateBusinessImpactAnalysisDocumentArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            .ForMember(dest => dest.DocumentId, act => act.MapFrom(source => source))
            ;
        CreateMap<CreateBusinessImpactAnalysisDisasterOriginCommand, CreateBusinessImpactAnalysisDisasterOriginArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;
        CreateMap<ModifyBusinessImpactAnalysisCommand, ModifyBusinessImpactAnalysisArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            ;
    }
}