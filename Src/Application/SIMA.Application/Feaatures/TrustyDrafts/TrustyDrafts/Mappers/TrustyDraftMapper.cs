using AutoMapper;
using SIMA.Application.Contract.Features.TrustyDrafts.TrustyDrafts;
using SIMA.Application.Services.BehsazanServices.Response;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Args;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Args;
using SIMA.Framework.Common.Helper;
using System.Globalization;
using System.Text;

namespace SIMA.Application.Feaatures.TrustyDrafts.TrustyDrafts.Mappers;

public class TrustyDraftMapper : Profile
{
    public TrustyDraftMapper()
    {
        CreateMap<CreateTrustyDraftCommand, CreateTrustyDraftArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;

        CreateMap<CreateTrustyDraftCommand, CreateFinalTrustyDraftArg>()
           .ForMember(dest => dest.IssueId, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           ;

        CreateMap<ModifyTrustyDraftCommand, ModifyTrustyDraftArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())))
            //.ForMember(dest => dest.IssueDueDate, act => act.MapFrom(source => DateHelper.ToMiladiDate(source.IssueDueDate)))
            ;

        CreateMap<CreateDraftDocument, CreateTrustyDraftDocumentArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;


        CreateMap<GetTrustCurrencyDraftMansha, CreateCurrencyTypeArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Code, act => act.MapFrom(source => CodeGenerator.GeneratedCode()))
            .ForMember(dest => dest.Name, act => act.MapFrom(source => source.Data[0].instructedArzCd))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;

        CreateMap<GetTrustCurrencyDraftMansha, CreateDraftStatusArg>()
            .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
            .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
            .ForMember(dest => dest.Code, act => act.MapFrom(source => CodeGenerator.GeneratedCode()))
            .ForMember(dest => dest.Name, act => act.MapFrom(source => source.Data[0].draftSts))
            .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
            ;

        CreateMap<GetTrustCurrencyDraftMansha, CreateTrustyDraftArg>()
           .ForMember(dest => dest.Id, act => act.MapFrom(source => IdHelper.GenerateUniqueId()))
           .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
           .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now))
           .ForMember(dest => dest.BeneficiaryName, act => act.MapFrom(source => source.Data[0].benName))
           .ForMember(dest => dest.BlockingNumber, act => act.MapFrom(source => source.Data[0].blockAdeNo))
           .ForMember(dest => dest.DraftNumberBasedOnOrder, act => act.MapFrom(source => source.Data[0].draftId))
           .ForMember(dest => dest.DraftOrderNumber, act => act.MapFrom(source => source.Data[0].orderId))
           .ForMember(dest => dest.DraftIssueDate, act => act.MapFrom(source => source.Data[0].regDate > 0 ? DateHelper.ToMiladiDate(source.Data[0].regDate.ToString()) : (DateTime?)null))
           .ForMember(dest => dest.DraftNetAmount, act => act.MapFrom(source => source.Data[0].settlementAmnt))
           .ForMember(dest => dest.PayingBankName, act => act.MapFrom(source => source.Data[0].payerBank))
           .ForMember(dest => dest.IntermediaryBank, act => act.MapFrom(source => source.Data[0].interMedBankName))
           .ForMember(dest => dest.BeneficiaryAddress, act => act.MapFrom(source => source.Data[0].benAddress))
           .ForMember(dest => dest.BeneficiaryPhoneNumber, act => act.MapFrom(source => source.Data[0].benTel))
           .ForMember(dest => dest.BeneficiaryAccountNumber, act => act.MapFrom(source => source.Data[0].orderingIBAN))
           .ForMember(dest => dest.BeneficiaryPassportNumber, act => act.MapFrom(source => source.Data[0].benPassport))
           .ForMember(dest => dest.BeneficiaryExternalAccountNumber, act => act.MapFrom(source => source.Data[0].benExtAccNo))
           .ForMember(dest => dest.BeneficiaryIban, act => act.MapFrom(source => source.Data[0].benIban))
           .ForMember(dest => dest.DraftRequestAmount, act => act.MapFrom(source => source.Data[0].instructedAmnt))
           .ForMember(dest => dest.DraftRequestAmountBasedOnUsd, act => act.MapFrom(source => source.Data[0].bInstructedAmnt))
           .ForMember(dest => dest.DraftRequestNetAmountBasedOnUsd, act => act.MapFrom(source => source.Data[0].bSettlementAmnt))
           .ForMember(dest => dest.RejectReason, act => act.MapFrom(source => source.Data[0].disapprovalReasonCd))
           .ForMember(dest => dest.Description, act => act.MapFrom(source => source.Data[0].remittanceInfoDesc))
           .ForMember(dest => dest.CancellationDate, act => act.MapFrom(source => source.Data[0].revokeDt > 0 ? DateHelper.ToMiladiDate(source.Data[0].revokeDt.ToString()) : (DateTime?)null))
           .ForMember(dest => dest.CancellationValorNumber, act => act.MapFrom(source => source.Data[0].revokeValueDt))
           .ForMember(dest => dest.CancellationAmount, act => act.MapFrom(source => source.Data[0].revokeArzAmnt))
           .ForMember(dest => dest.CancellationReferenceNumber, act => act.MapFrom(source => source.Data[0].revokeDraftRefNo))
           .ForMember(dest => dest.BlockingAmount, act => act.MapFrom(source => source.Data[0].blockedAmnt))
           .ForMember(dest => dest.CustomerNationalCode, act => act.MapFrom(source => source.Data[0].nationalCode))
           .ForMember(dest => dest.CustomerPhoneNumber, act => act.MapFrom(source => source.Data[0].orderingTel))
           .ForMember(dest => dest.CustomerAddress, act => act.MapFrom(source => source.Data[0].orderingAddress))
           .ForMember(dest => dest.DraftAcceptTime, act => act.MapFrom(source => source.Data[0].confirmTime > 0 ? DateHelper.ToTimeOnly(source.Data[0].confirmTime.ToString()) : (TimeOnly?)null))
           .ForMember(dest => dest.DraftAcceptDate, act => act.MapFrom(source => source.Data[0].confirmDate > 0 ? DateHelper.ToMiladiDate(source.Data[0].confirmDate.ToString()) : (DateTime?)null))
           .ForMember(dest => dest.IssueReason, act => act.MapFrom(source => source.Data[0].draftReasonDesc))
           .ForMember(dest => dest.AgentBank, act => act.MapFrom(source => source.Data[0].agentShareStsMsg))
           .ForMember(dest => dest.BrokerBankName, act => act.MapFrom(source => source.Data[0].crspndntBic))
           .ForMember(dest => dest.CustomerAccountNumber, act => act.MapFrom(source => source.Data[0].orderingExtAccNo))
           .ForMember(dest => dest.ValorDate, act => act.MapFrom(source => source.Data[0].valueDate > 0 ? DateHelper.ConvertMiladiToDateTime(source.Data[0].valueDate.ToString()) : (DateTime?)null))
           .ForMember(dest => dest.DraftNumber, act => act.MapFrom(source => source.Data[0].draftId))
        //.ForMember(dest => dest.OrderingExternalAccountNumber, act => act.MapFrom(source => source.Data[0].orderingExtAccNo));



        ;
    }
}
