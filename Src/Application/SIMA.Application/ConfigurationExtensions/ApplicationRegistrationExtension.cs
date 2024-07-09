using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using SIMA.Application.Feaatures.Auths.AddressTypes.Mappers;
using SIMA.Application.Feaatures.Auths.Companies;
using SIMA.Application.Feaatures.Auths.Companies.Mappers;
using SIMA.Application.Feaatures.Auths.ConfigurationAttributes.Mappers;
using SIMA.Application.Feaatures.Auths.Departments.Mappers;
using SIMA.Application.Feaatures.Auths.Forms.Mappers;
using SIMA.Application.Feaatures.Auths.Genders.Mappers;
using SIMA.Application.Feaatures.Auths.Groups.Mappers;
using SIMA.Application.Feaatures.Auths.Locations.Mappers;
using SIMA.Application.Feaatures.Auths.LocationTypes.Mappers;
using SIMA.Application.Feaatures.Auths.Permision.Mappers;
using SIMA.Application.Feaatures.Auths.PhoneTypes.Mappers;
using SIMA.Application.Feaatures.Auths.Positions.Mappers;
using SIMA.Application.Feaatures.Auths.Profiles.Mappers;
using SIMA.Application.Feaatures.Auths.Roles.Mappers;
using SIMA.Application.Feaatures.Auths.Staffs.Mappers;
using SIMA.Application.Feaatures.Auths.SysConfigs.Mappers;
using SIMA.Application.Feaatures.Auths.Users.Mappers;
using SIMA.Application.Feaatures.BCP.Consequences.Mappers;
using SIMA.Application.Feaatures.BCP.HappeningPossiblities.Mappers;
using SIMA.Application.Feaatures.BCP.ImportanceDegrees.Mappers;
using SIMA.Application.Feaatures.BCP.RecoveryPointObjectives.Mappers;
using SIMA.Application.Feaatures.BCP.ServicePriorities.Mapper;
using SIMA.Application.Feaatures.BranchManagement.Branches.Mappers;
using SIMA.Application.Feaatures.BranchManagement.BranchTypes.Mappers;
using SIMA.Application.Feaatures.BranchManagement.Brokers.Mappers;
using SIMA.Application.Feaatures.BranchManagement.BrokerTypes.Mapper;
using SIMA.Application.Feaatures.BranchManagement.CurrencyTypes.Mapper;
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
using SIMA.Application.Feaatures.Logistics.GoodsTypes.Mappers;
using SIMA.Application.Feaatures.Logistics.LogisticRequests.Mapper;
using SIMA.Application.Feaatures.Logistics.SupplierRanks.Mappers;
using SIMA.Application.Feaatures.Logistics.Suppliers.Mappers;
using SIMA.Application.Feaatures.Logistics.UnitMeasurements.Mappers;
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
using SIMA.Application.Feaatures.ServiceCatalog.ChannelTypes.Mappers;
using SIMA.Application.Feaatures.ServiceCatalog.ServiceBoundles.Mappers;
using SIMA.Application.Feaatures.ServiceCatalog.ServiceCategories.Mappers;
using SIMA.Application.Feaatures.ServiceCatalog.ServiceCustomerTypes.Mappers;
using SIMA.Application.Feaatures.ServiceCatalog.ServiceTypes.Mappers;
using SIMA.Application.Feaatures.ServiceCatalog.ServiceUserTypes.Mappers;
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

namespace SIMA.Application.ConfigurationExtensions;

public static class ApplicationRegistrationExtension
{
    public static IServiceCollection RegisterCommandMappers(this IServiceCollection services)
    {

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
                conf.AddProfile(new SubjectPriorityMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new MeetingHoldingReasonMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new MeetingHoldingStatusMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new MeetingMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                #endregion

                #region RiskManagement
                conf.AddProfile(new ImpactScaleMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new RiskDegreeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new RiskImpactMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new RiskLevelMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new RiskPossibilityMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new RiskTypeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new ThreatTypeMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new RiskMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new RiskLevelMeasureMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>()));
                conf.AddProfile(new RiskCriteriaMapper(scopedServiceProvider.GetRequiredService<ISimaIdentity>())); ;

                #endregion

                #region ServiceCatalog
                conf.AddProfile(new ServiceTypeMapper());
                conf.AddProfile(new ServiceCustomerTypeMapper());
                conf.AddProfile(new ServiceCategoryMapper());
                conf.AddProfile(new ServiceBoundleMapper());
                conf.AddProfile(new ServiceUserTypeMapper());
                conf.AddProfile(new ChannelTypeMapper());
                #endregion

                #region BCP
                conf.AddProfile(new ImportanceDegreeMapper());
                conf.AddProfile(new OrganizationalServicePriorityMapper());
                conf.AddProfile(new HappeningPossibilityMapper());
                conf.AddProfile(new ConsequenceMapper());
                conf.AddProfile(new RecoveryPointObjectiveMapper());
                #endregion

                #region Logistics
                conf.AddProfile(new UnitMeasurementMapper());
                conf.AddProfile(new GoodsMapper());
                conf.AddProfile(new GoodsTypeMapper());
                conf.AddProfile(new GoodsQuorumPriceMapper());
                conf.AddProfile(new SupplierMapper());
                conf.AddProfile(new SupplierRankMapper());
                conf.AddProfile(new GoodsCategoryMapper());
                conf.AddProfile(new LogisticRequestMapper());
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
