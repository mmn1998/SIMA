#region using
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SIMA.Application.Feaatures.AssetAndConfigurations.AssetPhysicalStatuses.Mappers;
using SIMA.Application.Feaatures.AssetAndConfigurations.Assets.Mapper;
using SIMA.Application.Feaatures.AssetAndConfigurations.AssetTechnicalStatuses.Mappers;
using SIMA.Application.Feaatures.AssetAndConfigurations.AssetTypes.Mappers;
using SIMA.Application.Feaatures.AssetAndConfigurations.BusinessCriticalities.Mappers;
using SIMA.Application.Feaatures.AssetAndConfigurations.ConfigurationItemRelationshipTypes.Mappers;
using SIMA.Application.Feaatures.AssetAndConfigurations.ConfigurationItemStatuses.Mappers;
using SIMA.Application.Feaatures.Auths.AccessTypes.Mappers;
using SIMA.Application.Feaatures.Auths.AddressTypes.Mappers;
using SIMA.Application.Feaatures.Auths.ApiMethodActions.Mappers;
using SIMA.Application.Feaatures.Auths.Companies;
using SIMA.Application.Feaatures.Auths.Companies.Mappers;
using SIMA.Application.Feaatures.Auths.ConfigurationAttributes.Mappers;
using SIMA.Application.Feaatures.Auths.CustomerTypes.Mappers;
using SIMA.Application.Feaatures.Auths.Departments.Mappers;
using SIMA.Application.Feaatures.Auths.Forms.Mappers;
using SIMA.Application.Feaatures.Auths.Genders.Mappers;
using SIMA.Application.Feaatures.Auths.Groups.Mappers;
using SIMA.Application.Feaatures.Auths.Locations.Mappers;
using SIMA.Application.Feaatures.Auths.LocationTypes.Mappers;
using SIMA.Application.Feaatures.Auths.Permision.Mappers;
using SIMA.Application.Feaatures.Auths.PhoneTypes.Mappers;
using SIMA.Application.Feaatures.Auths.PositionLevels.Mappers;
using SIMA.Application.Feaatures.Auths.Positions.Mappers;
using SIMA.Application.Feaatures.Auths.PositionTypes.Mappers;
using SIMA.Application.Feaatures.Auths.Profiles.Mappers;
using SIMA.Application.Feaatures.Auths.ResponsibleTypes.Mappers;
using SIMA.Application.Feaatures.Auths.Roles.Mappers;
using SIMA.Application.Feaatures.Auths.Staffs.Mappers;
using SIMA.Application.Feaatures.Auths.SupplierRanks.Mappers;
using SIMA.Application.Feaatures.Auths.Suppliers.Mappers;
using SIMA.Application.Feaatures.Auths.SysConfigs.Mappers;
using SIMA.Application.Feaatures.Auths.TimeMeasurements.Mappers;
using SIMA.Application.Feaatures.Auths.UIInputElements.Mappers;
using SIMA.Application.Feaatures.Auths.Users.Mappers;
using SIMA.Application.Feaatures.BCP.Back_UpPeriods.Mappers;
using SIMA.Application.Feaatures.BCP.BusinesImpactAnalysises.Mappers;
using SIMA.Application.Feaatures.BCP.BusinessContinuityPlans.Mapper;
using SIMA.Application.Feaatures.BCP.BusinessContinuityStrategies.Mappers;
using SIMA.Application.Feaatures.BCP.Consequences.Mappers;
using SIMA.Application.Feaatures.BCP.HappeningPossiblities.Mappers;
using SIMA.Application.Feaatures.BCP.ImportanceDegrees.Mappers;
using SIMA.Application.Feaatures.BCP.Origins.Mappers;
using SIMA.Application.Feaatures.BCP.PlanResponsibilities.Mappers;
using SIMA.Application.Feaatures.BCP.RecoveryOptionPriorities.Mappers;
using SIMA.Application.Feaatures.BCP.RecoveryPointObjectives.Mappers;
using SIMA.Application.Feaatures.BCP.ScenarioExecutionHistories.Mapper;
using SIMA.Application.Feaatures.BCP.Scenarios.Mappers;
using SIMA.Application.Feaatures.BCP.ServicePriorities.Mapper;
using SIMA.Application.Feaatures.BCP.StrategyTypes.Mappers;
using SIMA.Application.Feaatures.BranchManagement.AccountTypes.Mappers;
using SIMA.Application.Feaatures.BranchManagement.Branches.Mappers;
using SIMA.Application.Feaatures.BranchManagement.BranchTypes.Mappers;
using SIMA.Application.Feaatures.BranchManagement.Brokers.Mappers;
using SIMA.Application.Feaatures.BranchManagement.BrokerTypes.Mapper;
using SIMA.Application.Feaatures.BranchManagement.CurrencyOprationTypes.Mapper;
using SIMA.Application.Feaatures.BranchManagement.CurrencyTypes.Mapper;
using SIMA.Application.Feaatures.BranchManagement.Customers.Mappers;
using SIMA.Application.Feaatures.BranchManagement.FinancialActionTypes.Mapper;
using SIMA.Application.Feaatures.BranchManagement.LoanTypes.Mappers;
using SIMA.Application.Feaatures.BranchManagement.PaymentTypes.Mappers;
using SIMA.Application.Feaatures.DMS.Documents.Mappers;
using SIMA.Application.Feaatures.DMS.DocumentsExtensions.Mappers;
using SIMA.Application.Feaatures.DMS.DocumentTypes.Mappers;
using SIMA.Application.Feaatures.DMS.WorkFlowDocumentTypes.Mappers;
using SIMA.Application.Feaatures.IssueManagement.IssueLinkReasons.Mapper;
using SIMA.Application.Feaatures.IssueManagement.IssuePriorities.Mappers;
using SIMA.Application.Feaatures.IssueManagement.Issues.Mapper;
using SIMA.Application.Feaatures.IssueManagement.IssueTypes.Mappers;
using SIMA.Application.Feaatures.IssueManagement.IssueWeightCategories.Mappers;
using SIMA.Application.Feaatures.Logistics.GoodsCategories.Mappers;
using SIMA.Application.Feaatures.Logistics.Goodses.Mappers;
using SIMA.Application.Feaatures.Logistics.GoodsQuorumPrices.Mappers;
using SIMA.Application.Feaatures.Logistics.GoodsStatues.Mappers;
using SIMA.Application.Feaatures.Logistics.GoodsTypes.Mappers;
using SIMA.Application.Feaatures.Logistics.LogisticRequests.Mapper;
using SIMA.Application.Feaatures.Logistics.LogisticsSupplies.Mappers;
using SIMA.Application.Feaatures.Logistics.UnitMeasurements.Mappers;
using SIMA.Application.Feaatures.Notifications.Messages.Mapper;
using SIMA.Application.Feaatures.RiskManagers.EvaluationCriterias.Mappers;
using SIMA.Application.Feaatures.RiskManagers.ImpactScales.Mapper;
using SIMA.Application.Feaatures.RiskManagers.RiskCriterias.Mapper;
using SIMA.Application.Feaatures.RiskManagers.RiskDegrees.Mapper;
using SIMA.Application.Feaatures.RiskManagers.RiskImpacts.Mapper;
using SIMA.Application.Feaatures.RiskManagers.RiskLevelMeasures.Mapper;
using SIMA.Application.Feaatures.RiskManagers.RiskLevels.Mapper;
using SIMA.Application.Feaatures.RiskManagers.RiskPossibilities.Mapper;
using SIMA.Application.Feaatures.RiskManagers.Risks.Mapper;
using SIMA.Application.Feaatures.RiskManagers.RiskTypes.Mapper;
using SIMA.Application.Feaatures.RiskManagers.ThreatTypes.Mapper;
using SIMA.Application.Feaatures.SecurityCommitees.Labels;
using SIMA.Application.Feaatures.SecurityCommitees.MeetingHoldingReasons.Mappers;
using SIMA.Application.Feaatures.SecurityCommitees.MeetingHoldingStatus.Mapper;
using SIMA.Application.Feaatures.SecurityCommitees.Meetings.Mapper;
using SIMA.Application.Feaatures.SecurityCommitees.SubjectPriorities.Mappers;
using SIMA.Application.Feaatures.ServiceCatalog.ApiAuthenticationMethods.Mapper;
using SIMA.Application.Feaatures.ServiceCatalog.ApiTypes.Mapper;
using SIMA.Application.Feaatures.ServiceCatalog.Channels.Mappers;
using SIMA.Application.Feaatures.ServiceCatalog.CriticalActivities.Mappers;
using SIMA.Application.Feaatures.ServiceCatalog.Products.Mapper;
using SIMA.Application.Feaatures.ServiceCatalog.ServiceCategories.Mappers;
using SIMA.Application.Feaatures.ServiceCatalog.ServicePriorities.Mapper;
using SIMA.Application.Feaatures.ServiceCatalog.Services.Mappers;
using SIMA.Application.Feaatures.ServiceCatalog.ServiceStatuses.Mapper;
using SIMA.Application.Feaatures.ServiceCatalog.ServiceUserTypes.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.DraftDestinations.Mapper;
using SIMA.Application.Feaatures.TrustyDrafts.CancellationResaons.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.DraftIssueTypes.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.DraftOrigins.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.DraftReviewResults.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.DraftTypes.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.DraftValorStatuses.Mappers;
using SIMA.Application.Feaatures.WorkFlowEngine.ApprovalOptions.Mapper;
using SIMA.Application.Feaatures.WorkFlowEngine.BPMSes.Mappers;
using SIMA.Application.Feaatures.WorkFlowEngine.Progress.Mapper;
using SIMA.Application.Feaatures.WorkFlowEngine.Project.Mapper;
using SIMA.Application.Feaatures.WorkFlowEngine.WorkFlow.Mapper;
using SIMA.Application.Feaatures.WorkFlowEngine.WorkFlowActor.Mappers;
using SIMA.Application.Feaatures.WorkFlowEngine.WorkFlowCompany.Mapper;
using SIMA.Framework.Common.Helper.FileHelper;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Application.Feaatures.TrustyDrafts.ResponsibilityWageTypes.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.RequestValors.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.TrustyDrafts.Mappers;
using SIMA.Application.Feaatures.AssetAndConfigurations.LicenseTypes.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.DraftCurrencyOrigins.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.BrokerInquiryStatuses.Mapper;
using SIMA.Application.Feaatures.BranchManagement.FinancialSuppliers.Mapper;
using SIMA.Application.Feaatures.TrustyDrafts.Resources.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.WageRates.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.CurrencyPaymentChannels.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.BrokerSecondLevelAddressBooks.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.WageDeductionMethods.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.InquiryRequests.Mapper;
using SIMA.Application.Feaatures.TrustyDrafts.InquiryResponses.Mapper;
using SIMA.Application.Feaatures.TrustyDrafts.AgentBankWageShareStatuses.Mappers;
using SIMA.Application.Feaatures.TrustyDrafts.ReferralLetters.Mapper;
using SIMA.Application.Feaatures.RiskManagers.ConsequenceCategories.Mappers;
using SIMA.Application.Feaatures.RiskManagers.ConsequenceLevels.Mappers;
using SIMA.Application.Feaatures.RiskManagers.TriggerStatuses.Mapper;
using SIMA.Application.Feaatures.RiskManagers.UseVulnerabilities.Mapper;
using SIMA.Domain.Models.Features.RiskManagement.UseVulnerabilities.Entities;
using SIMA.Application.Feaatures.RiskManagers.AffectedHistories.Mappers;
using SIMA.Application.Feaatures.RiskManagers.ScenarioHistories.Mapper;
using SIMA.Application.Feaatures.RiskManagers.SeverityValues.Mappers;
using SIMA.Application.Feaatures.RiskManagers.Severities.Mappers;
using SIMA.Application.Feaatures.RiskManagers.MatrixAs.Mappers;
using SIMA.Application.Feaatures.RiskManagers.InherentOccurrenceProbabilityValues.Mappers;

#endregion

namespace SIMA.Application.ConfigurationExtensions;

public static class ApplicationRegistrationExtension
{
    public static IServiceCollection RegisterCommandMappers(this IServiceCollection services)
    {
       //services.AddAutoMapper(typeof(LogisticRequestMapper).Assembly);
       return services.AddAutoMapper((serviceProvider, conf) =>
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var scopedServiceProvider = scope.ServiceProvider;
                // Add profiles...
                #region Auths
                conf.AddProfile(new CompanyMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new AddressTypeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new ConfigurationttributeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new DepartmentMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new GenderMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new GroupMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new LocationMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new LocationTypeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new PermisionMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new PhoneTypeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new PositionMappers(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new ProfileMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new RoleMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new StaffMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new SysConfigMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new UserMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new FormMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new UIInputElementMapper());
                conf.AddProfile(new ApiMethodActionMapper());
                conf.AddProfile(new CustomerTypeMapper());
                conf.AddProfile(new PositionTypeMapper());
                conf.AddProfile(new PositionLevelMapper());
                conf.AddProfile(new ResponsibleTypeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new SupplierMapper());
                conf.AddProfile(new SupplierRankMapper());
                conf.AddProfile(new AccessTypeMapper());
                conf.AddProfile(new TimeMeasurementMapper());
                #endregion

                #region WorkFlows

                conf.AddProfile(new ProgressMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new WorkFlowMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new WorkFlowActorMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new ProjectMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new BpmsMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new WorkFlowCompanyMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new ApprovalOptionMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));

                #endregion

                #region Issue

                conf.AddProfile(new IssuePriorityMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new IssueWeightCategoryMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new IssueLinkReasonMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new IssueTypeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new IssueMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));

                #endregion

                #region Branchs

                conf.AddProfile(new CurrencyTypeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new PaymentTypeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new BrokerTypeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new BranchTypeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new BranchMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new BrokerMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new AccountTypeMapper());
                conf.AddProfile(new LoanTypeMapper());
                conf.AddProfile(new CustomerMapper());
                conf.AddProfile(new CurrencyOprationTypeMapper());
                conf.AddProfile(new FinancialActionTypeMapper());
                conf.AddProfile(new FinancialSupplierMapper());

                #endregion

                #region DMS
                conf.AddProfile(new DocumentsExtensionMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new DocumentTypesMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new WorkFlowDocumentTypesMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new DocumentMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>(),
                    scopedServiceProvider.GetRequiredService<IWebHostEnvironment>(),
                    scopedServiceProvider.GetRequiredService<IFileService>(),
                    serviceProvider));
                #endregion

                #region SecurityCommitees
                conf.AddProfile(new SubjectPriorityMapper());
                conf.AddProfile(new MeetingHoldingReasonMapper());
                conf.AddProfile(new MeetingHoldingStatusMapper());
                conf.AddProfile(new MeetingMapper());
                #endregion

                #region RiskManagement
                conf.AddProfile(new ImpactScaleMapper());
                conf.AddProfile(new ConsequenceCategoryMapper());
                conf.AddProfile(new AffectedHistoryMapper());
                conf.AddProfile(new SeverityMapper());
                conf.AddProfile(new MatrixAMapper());
                conf.AddProfile(new SeverityValueMapper());
                conf.AddProfile(new InherentOccurrenceProbabilityValueMapper());
                conf.AddProfile(new ConsequenceLevelMapper());
                conf.AddProfile(new RiskDegreeMapper());
                conf.AddProfile(new RiskImpactMapper());
                conf.AddProfile(new RiskLevelMapper());
                conf.AddProfile(new RiskPossibilityMapper());
                conf.AddProfile(new RiskTypeMapper());
                conf.AddProfile(new ThreatTypeMapper());
                conf.AddProfile(new RiskMapper());
                conf.AddProfile(new RiskLevelMeasureMapper());
                conf.AddProfile(new RiskCriteriaMapper()); ;
                conf.AddProfile(new EvaluationCriteriaMapper());
                conf.AddProfile(new TriggerStatusMapper());
                conf.AddProfile(new UseVulnerabilityMapper());
                conf.AddProfile(new ScenarioHistoryMapper());
                #endregion

                #region ServiceCatalog
                conf.AddProfile(new ServiceCategoryMapper());
                conf.AddProfile(new UserTypeMapper());
                conf.AddProfile(new ApiAuthenticationMethodMapper());
                conf.AddProfile(new ServiceStatusMapper());
                conf.AddProfile(new ApiTypeMapper());
                conf.AddProfile(new ChannelMapper());
                conf.AddProfile(new ServiceMapper());
                conf.AddProfile(new ProductMapper());
                conf.AddProfile(new CriticalActivityMapper());
                conf.AddProfile(new ServicePriorityMapper());
                #endregion

                #region BCP
                conf.AddProfile(new ImportanceDegreeMapper());
                conf.AddProfile(new OrganizationalServicePriorityMapper());
                conf.AddProfile(new HappeningPossibilityMapper());
                conf.AddProfile(new ConsequenceMapper());
                conf.AddProfile(new RecoveryPointObjectiveMapper());
                conf.AddProfile(new OriginMapper());
                conf.AddProfile(new PlanResponsibilityMapper());
                conf.AddProfile(new StrategyTypeMapper());
                conf.AddProfile(new BackupPeriodMapper());
                conf.AddProfile(new RecoveryOptionPriorityMapper());
                conf.AddProfile(new BusinessImpactAnalysisMapper());
                conf.AddProfile(new BusinessContinuityStrategyMapper());
                conf.AddProfile(new ScenarioMapper());
                conf.AddProfile(new ScenarioExecutionHistoryMapper());
                conf.AddProfile(new BusinessContinuityPlanMapper());
                #endregion

                #region Logistics
                conf.AddProfile(new UnitMeasurementMapper());
                conf.AddProfile(new GoodsMapper());
                conf.AddProfile(new GoodsTypeMapper());
                conf.AddProfile(new GoodsQuorumPriceMapper());

                conf.AddProfile(new GoodsCategoryMapper());
                conf.AddProfile(new LogisticRequestMapper());
                conf.AddProfile(new LogisticsSupplyMapper());
                conf.AddProfile(new GoodsStatusMapper());
                #endregion

                #region TrustyDrafts
                conf.AddProfile(new TrustyDraftMapper());
                conf.AddProfile(new DraftCurrencyOriginMapper());
                conf.AddProfile(new DraftOriginMapper());
                conf.AddProfile(new WageDeductionMethodMapper());
                conf.AddProfile(new BrokerSecondLevelAddressBookMapper());
                conf.AddProfile(new CurrencyPaymentChannelMapper());
                conf.AddProfile(new WageRateMapper());
                conf.AddProfile(new ResourceMapper());
                conf.AddProfile(new RequestValorMapper());
                conf.AddProfile(new ResponsibilityWageTypeMapper());
                conf.AddProfile(new DraftReviewResultMapper());
                conf.AddProfile(new CancellationResaonMapper());
                conf.AddProfile(new DraftTypeMapper());
                conf.AddProfile(new ReconsilationTypeMapper());
                conf.AddProfile(new DraftIssueTypeMapper());
                conf.AddProfile(new DraftValorStatusMapper());
                conf.AddProfile(new DraftStatusMapper());
                conf.AddProfile(new DraftDestinationMapper());
                conf.AddProfile(new BrokerInquiryStatusMapper());
                conf.AddProfile(new AgentBankWageShareStatusMapper());
                conf.AddProfile(new InquiryRequestMapper());
                conf.AddProfile(new InquiryResponseMapper());
                conf.AddProfile(new InquiryResponseMapper());
                conf.AddProfile(new ReferralLetterMapper());
                #endregion

                #region AssetAndConfigurations
                conf.AddProfile(new AssetMapper());
                conf.AddProfile(new AssetPhysicalStatusMapper());
                conf.AddProfile(new AssetTechnicalStatusMapper());
                conf.AddProfile(new BusinessCriticalityMapper());
                conf.AddProfile(new ConfigurationItemStatusMapper());
                conf.AddProfile(new AssetTypeMapper());
                conf.AddProfile(new ConfigurationItemTypeMapper());
                conf.AddProfile(new ConfigurationItemRelationshipTypeMapper());
                conf.AddProfile(new LicenseTypeMapper());
                #endregion

                #region Notification
                conf.AddProfile(new MessageMapper());

                #endregion
            }
        }, Array.Empty<Type>());

    }
    public static IServiceCollection AddCommandHandlerServices(this IServiceCollection services)
    {
        services.Scan(scan =>
          scan.FromAssemblyOf<LabelEventHandler>()
          .AddClasses(classes => classes.AssignableTo(typeof(INotificationHandler<>)))
          .AsImplementedInterfaces()
          .WithScopedLifetime());

        return services.Scan(scan =>
            scan.FromAssemblyOf<CompanyCommandHandler>()
            .AddClasses(classes => classes.AssignableTo(typeof(ICommandHandler<,>)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());
    }
}
