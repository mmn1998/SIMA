using AutoMapper;
using SIMA.Application.Contract.Features.IssueManagement.Issues;
using SIMA.Application.Query.Contract.Features.WorkFlowEngine.WorkFlow.grpc;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Events;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Events;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Events;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Args;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Events;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Events;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Events;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Events;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Events;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Events;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Events;

namespace SIMA.Application.Feaatures.IssueManagement.Issues.Mapper;

public class IssueMapper : Profile
{
    public IssueMapper(ISimaIdentity simaIdentity)
    {
        CreateMap<CreateIssueArg, CreateIssueChangeHistoryArg>();

        CreateMap<ModifyIssueArg, CreateIssueChangeHistoryArg>()
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.CreatedBy, act => act.MapFrom(source => source.ModifiedBy));

        CreateMap<CreateIssueCommand, CreateIssueArg>()
    .ForMember(x => x.Id, act => act.MapFrom(src => IdHelper.GenerateUniqueId()))
    .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
    .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
    .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
    .ForMember(dest => dest.DueDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.DueDate)))
    .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<CreateIssueCommand, CreateIssueHistoryArg>()
            .ForMember(x => x.Id, act => act.MapFrom(src => IdHelper.GenerateUniqueId()))
            .ForMember(x => x.PerformerUserId, act => act.MapFrom(src => simaIdentity.UserId))
            .ForMember(x => x.CreatedAt, act => act.MapFrom(src => DateTime.Now))
            .ForMember(x => x.Description, act => act.MapFrom(src => src.Description))
            .ForMember(x => x.Name, act => act.MapFrom(src => src.Summery));

        CreateMap<Issue, CreateIssueHistoryArg>()
           .ForMember(x => x.Id, act => act.MapFrom(src => IdHelper.GenerateUniqueId()))
           .ForMember(x => x.PerformerUserId, act => act.MapFrom(src => simaIdentity.UserId))
           .ForMember(x => x.CreatedAt, act => act.MapFrom(src => DateTime.Now))
           .ForMember(x => x.Description, act => act.MapFrom(src => src.Description))
           .ForMember(x => x.Name, act => act.MapFrom(src => src.Summery));

        CreateMap<CreateIssueArg, CreateIssueHistoryArg>()
           .ForMember(x => x.Id, act => act.MapFrom(src => IdHelper.GenerateUniqueId()))
           .ForMember(x => x.PerformerUserId, act => act.MapFrom(src => simaIdentity.UserId))
           .ForMember(x => x.CreatedAt, act => act.MapFrom(src => DateTime.Now))
           .ForMember(x => x.Description, act => act.MapFrom(src => src.Description))
           .ForMember(x => x.Name, act => act.MapFrom(src => src.Summery));
        CreateMap<InputParamModel, InputModel>();
        CreateMap<IssueRunActionCommand, GetNextStepQuery>();

        CreateMap<ModifyIssueCommand, ModifyIssueArg>()
            .ForMember(dest => dest.DueDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.DueDate)))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));

        CreateMap<ModifyTrustyDraftEvent, ModifyIssueArg>()
            .ForMember(dest => dest.DueDate, act => act.MapFrom(source => source.DueDate))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));

        CreateMap<IssueRunActionCommand, IssueRunActionArg>()
        .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));

        CreateMap<CreateIssueLinkCommand, CreateIssueLinkArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));


        CreateMap<CreateIssueDocumentCommand, CreateIssueDocumentArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<CreateIssueCommentCommand, CreateIssueCommentArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));


        CreateMap<IssueRunActionCommand, CreateIssueCommentArg>()
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

        CreateMap<MeetingCreatedEvent, CreateIssueArg>()
           .ForMember(x => x.Id, act => act.MapFrom(src => src.issueId))
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
           .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.Name))
           .ForMember(dest => dest.Description, act => act.MapFrom(source => source.Name))
           .ForMember(dest => dest.MainAggregateId, act => act.MapFrom(source => (long)source.MainAggregateType))
           ;

        CreateMap<CreateLogisticsRequestEvent, CreateIssueArg>()
           .ForMember(x => x.Id, act => act.MapFrom(src => src.issueId))
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
           .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.Name))
           .ForMember(dest => dest.Description, act => act.MapFrom(source => source.Name))
           .ForMember(dest => dest.RequesterId, act => act.MapFrom(source => source.RequesterId))
           .ForMember(dest => dest.MainAggregateId, act => act.MapFrom(source => (long)source.MainAggregateType))
           ;

        CreateMap<CreateCriticalActivityEvent, CreateIssueArg>()
           .ForMember(x => x.Id, act => act.MapFrom(src => src.issueId))
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
           .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.name))
           .ForMember(dest => dest.Description, act => act.MapFrom(source => source.name))
           .ForMember(dest => dest.MainAggregateId, act => act.MapFrom(source => (long)source.mainAggregateType))
           ;

        CreateMap<CreateBusinessContinuityPlanEvent, CreateIssueArg>()
           .ForMember(x => x.Id, act => act.MapFrom(src => src.issueId))
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
           .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.Name))
           .ForMember(dest => dest.Description, act => act.MapFrom(source => source.Name))
           .ForMember(dest => dest.SourceId, act => act.MapFrom(source => source.SourceId))
           .ForMember(dest => dest.MainAggregateId, act => act.MapFrom(source => (long)source.MainAggregateType))
           ;

        CreateMap<CreateLogisticsSupplyEvent, CreateIssueArg>()
           .ForMember(x => x.Id, act => act.MapFrom(src => src.issueId))
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
           .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.Name))
           .ForMember(dest => dest.Description, act => act.MapFrom(source => source.Name))
           .ForMember(dest => dest.RequesterId, act => act.MapFrom(source => source.RequesterId))
           .ForMember(dest => dest.MainAggregateId, act => act.MapFrom(source => (long)source.MainAggregateType))
           ;

        CreateMap<ModifyLogisticsRequestEvent, ModifyIssueArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.summery))
           .ForMember(dest => dest.Description, act => act.MapFrom(source => source.summery))
           .ForMember(dest => dest.IssuePriorityId, act => act.MapFrom(source => source.issuePriority))
           .ForMember(dest => dest.Weight, act => act.MapFrom(source => source.weight))
           .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
           ;

        CreateMap<ModifyServiceEvent, ModifyIssueArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.name))
           .ForMember(dest => dest.Description, act => act.MapFrom(source => source.name))
           .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
           ;

        CreateMap<ModifyCriticalActivityEvent, ModifyIssueArg>()
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.name))
           .ForMember(dest => dest.Description, act => act.MapFrom(source => source.name))
           .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
           ;
        CreateMap<CreateServiceEvent, CreateIssueArg>()
          .ForMember(x => x.Id, act => act.MapFrom(src => src.issueId))
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
          .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.name))
          .ForMember(dest => dest.Description, act => act.MapFrom(source => source.name))
          .ForMember(dest => dest.MainAggregateId, act => act.MapFrom(source => (long)source.mainAggregateType))
          ;
        CreateMap<CreateBusinessImpactAnalysisEvent, CreateIssueArg>()
          .ForMember(x => x.Id, act => act.MapFrom(src => src.issueId))
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
          .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.name))
          .ForMember(dest => dest.Description, act => act.MapFrom(source => source.name))
          .ForMember(dest => dest.MainAggregateId, act => act.MapFrom(source => (long)source.mainAggregateType))
          ;
        CreateMap<CreateBusinessContinuityStrategyEvent, CreateIssueArg>()
          .ForMember(x => x.Id, act => act.MapFrom(src => src.issueId))
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
          .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.name))
          .ForMember(dest => dest.Description, act => act.MapFrom(source => source.name))
          .ForMember(dest => dest.MainAggregateId, act => act.MapFrom(source => (long)source.mainAggregateType))
          ;
        CreateMap<ModifyRiskCreateEvents, CreateIssueArg>()
          .ForMember(x => x.Id, act => act.MapFrom(src => src.issueId))
          .ForMember(x => x.Summery, act => act.MapFrom(src => src.Name))
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
          .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.Description, act => act.MapFrom(source => source.Name))
          .ForMember(dest => dest.MainAggregateId, act => act.MapFrom(source => (long)source.MainAggregateType))
          ;
        CreateMap<RiskCreateEvents, CreateIssueArg>()
          .ForMember(x => x.Summery, act => act.MapFrom(src => src.Name))
          .ForMember(x => x.Description, act => act.MapFrom(src => src.Name))
          .ForMember(x => x.Id, act => act.MapFrom(src => src.issueId))
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.MainAggregateId, act => act.MapFrom(source => (long) source.MainAggregateType))
          .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
          .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          ;

        CreateMap<IssueRunActionCommand, CreateIssueApprovalArg>()
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.Description, act => act.MapFrom(source => source.ApprovalDescription))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          ;

        CreateMap<InputDocument, AddDocumentToSPQuery>()
         .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId().ToString()))
         .ForMember(dest => dest.DocumentId, act => act.MapFrom(source => source.DocumentId.ToString()))
         .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now.ToString()))
         .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => ((long)ActiveStatusEnum.Active).ToString()));

        CreateMap<long, CreateIssueManagerArg>()
          .ForMember(dest => dest.UserId, act => act.MapFrom(source => source))
          .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active));

        CreateMap<CreateTrustyDraftEvent, CreateIssueArg>()
          .ForMember(x => x.Id, act => act.MapFrom(src => src.issueId))
          .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
          .ForMember(dest => dest.CompanyId, act => act.MapFrom(source => simaIdentity.CompanyId))
          .ForMember(dest => dest.IssueDate, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
          .ForMember(dest => dest.Summery, act => act.MapFrom(source => source.Name))
          .ForMember(dest => dest.Description, act => act.MapFrom(source => source.Name))
          .ForMember(dest => dest.RequesterId, act => act.MapFrom(source => source.RequesterId))
          .ForMember(dest => dest.MainAggregateId, act => act.MapFrom(source => (long)source.MainAggregateType))
          ;

    }
}
