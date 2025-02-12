#region usings
using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetPhysicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTechnicalStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.AssetTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.BusinessCriticalities.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemRelationshipTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemStatuses.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.ConfigurationItemTypes.Entities;
using SIMA.Domain.Models.Features.AssetsAndConfigurations.LicenseTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AccessTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AdminLocationAccesses.Entities;
using SIMA.Domain.Models.Features.Auths.ApiMethodActions.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;
using SIMA.Domain.Models.Features.Auths.CustomerTypes.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Genders.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Entities;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Entities;
using SIMA.Domain.Models.Features.Auths.Permissions.Entities;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Entities;
using SIMA.Domain.Models.Features.Auths.PositionLevels.Entities;
using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.Auths.PositionTypes.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.ResponsibleTypes.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.SupplierRanks.Entities;
using SIMA.Domain.Models.Features.Auths.Suppliers.Entities;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Entities;
using SIMA.Domain.Models.Features.Auths.TimeMeasurements.Entities;
using SIMA.Domain.Models.Features.Auths.UIInputElements.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.UserTypes.Entities;
using SIMA.Domain.Models.Features.Auths.ViewLists.Entities;
using SIMA.Domain.Models.Features.BCP.Back_UpPeriods.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlans.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanStratgies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityPlanVersionings.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStategies.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgyResponsibles.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessContinuityStratgySolutions.Entities;
using SIMA.Domain.Models.Features.BCP.BusinessImpactAnalysises.Entities;
using SIMA.Domain.Models.Features.BCP.ConsequenceIntensions.Entities;
using SIMA.Domain.Models.Features.BCP.Consequences.Entities;
using SIMA.Domain.Models.Features.BCP.HappeningPossiblities.Entities;
using SIMA.Domain.Models.Features.BCP.ImportanceDegrees.Entities;
using SIMA.Domain.Models.Features.BCP.Origins.Entities;
using SIMA.Domain.Models.Features.BCP.PlanResponsibilities.Entities;
using SIMA.Domain.Models.Features.BCP.RecoveryOptionPriorities.Entities;
using SIMA.Domain.Models.Features.BCP.RecoveryPointObjectives.Entities;
using SIMA.Domain.Models.Features.BCP.ScenarioExecutionHistories.Entities;
using SIMA.Domain.Models.Features.BCP.Scenarios.Entities;
using SIMA.Domain.Models.Features.BCP.ServicePriorities.Entities;
using SIMA.Domain.Models.Features.BCP.SolutionPriorities.Entities;
using SIMA.Domain.Models.Features.BCP.StrategyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.AccountTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyOprationTypes.Enitites;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Customers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.FinancialActionTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.LoanTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Entities;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueCustomFeilds.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsCategories.Entities;
using SIMA.Domain.Models.Features.Logistics.Goodses.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsQuorumPrices.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsStatuses.Entities;
using SIMA.Domain.Models.Features.Logistics.GoodsTypes.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsRequests.Entities;
using SIMA.Domain.Models.Features.Logistics.LogisticsSupplies.Entities;
using SIMA.Domain.Models.Features.Logistics.UnitMeasurements.Entities;
using SIMA.Domain.Models.Features.Notifications.Messages.Entities;
using SIMA.Domain.Models.Features.RiskManagement.AffectedHistories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CobitScenarios.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceCategories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ConsequenceLevels.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.CurrentOccurrenceProbabilityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.EvaluationCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Frequencies.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ImpactScales.Entities;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.InherentOccurrenceProbabilityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAs.Entities;
using SIMA.Domain.Models.Features.RiskManagement.MatrixAValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskCriterias.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskDegrees.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelCobits.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevelMeasures.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskLevels.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskPossibilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Risks.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskTypes.Entities;
using SIMA.Domain.Models.Features.RiskManagement.RiskValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ScenarioHistories.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ServiceRiskImpacts.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Severities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.SeverityValues.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Threats.Entities;
using SIMA.Domain.Models.Features.RiskManagement.ThreatTypes.Entities;
using SIMA.Domain.Models.Features.RiskManagement.TriggerStatuses.Entities;
using SIMA.Domain.Models.Features.RiskManagement.UseVulnerabilities.Entities;
using SIMA.Domain.Models.Features.RiskManagement.Vulnerabilities.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Approvals.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Inviteeses.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingReasons.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingHoldingStatuses.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Meetings.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.MeetingSchedules.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.SubjectPriorities.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;
using SIMA.Domain.Models.Features.SecurityCommitees.SupervisorAnswerTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiAuthenticationMethods.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ApiTypes.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Channels.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.CriticalActivities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Products.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceCategories.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServicePriorities.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.Services.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceStatuses.Entities;
using SIMA.Domain.Models.Features.ServiceCatalogs.ServiceTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.AgentBankWageShareStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerInquiryStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.BrokerSecondLevelAddressBooks.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.CancellationResaons.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.CurrencyPaymentChannels.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftCurrencyOrigins.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftDestinations.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftIssueTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftOrigins.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftReviewResults.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.DraftValorStatuses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryRequests.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.InquiryResponses.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ReconsilationTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ReferralLetters.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.RequestValors.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.Resources.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.ResponsibilityWageTypes.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.TrustyDrafts.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.WageDeductionMethods.Entities;
using SIMA.Domain.Models.Features.TrustyDrafts.WageRates.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.ApprovalOptions.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Entities;
using SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowConfiguration;

#endregion

namespace SIMA.Persistance.Persistence;

public class SIMADBContext : DbContext
{
    public SIMADBContext()
    {

    }
    public SIMADBContext(DbContextOptions<SIMADBContext> options) : base(options)
    {

    }

    #region WorkFlowDBSets
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<ProjectGroup> ProjectGroups { get; set; }
    public virtual DbSet<ProjectMember> ProjectMembers { get; set; }
    public virtual DbSet<State> States { get; set; }
    public virtual DbSet<Step> Steps { get; set; }
    public virtual DbSet<WorkFlow> WorkFlows { get; set; }
    public virtual DbSet<WorkFlowActor> WorkFlowActors { get; set; }
    public virtual DbSet<WorkFlowActorGroup> WorkFlowActorGroups { get; set; }
    public virtual DbSet<WorkFlowActorRole> WorkFlowActorRoles { get; set; }
    public virtual DbSet<WorkFlowActorStep> WorkFlowActorSteps { get; set; }
    public virtual DbSet<WorkFlowActorUser> WorkFlowActorUsers { get; set; }
    public virtual DbSet<Progress> Progresses { get; set; }
    public virtual DbSet<ActionType> ActionTypes { get; set; }
    public virtual DbSet<WorkFlowCompany> WorkFlowCompanies { get; set; }
    public virtual DbSet<ApprovalOption> ApprovalOptions { get; set; }
    public virtual DbSet<StepApprovalOption> StepApprovalOptions { get; set; }
    #endregion

    #region BranchDbSets

    public virtual DbSet<SIMA.Domain.Models.Features.BranchManagement.Branches.Entities.Branch> Branches { get; set; }
    public virtual DbSet<BranchType> BranchTypes { get; set; }
    public virtual DbSet<Broker> Brokers { get; set; }
    public virtual DbSet<BrokerType> BrokerTypes { get; set; }
    public virtual DbSet<AccountType> AccountTypes { get; set; }
    public virtual DbSet<LoanType> LoanTypes { get; set; }
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }
    public virtual DbSet<PaymentType> PaymentTypes { get; set; }
    public virtual DbSet<FinancialActionType> FinancialActionTypes { get; set; }
    public virtual DbSet<FinancialSupplier> FinancialSuppliers { get; set; }
    public virtual DbSet<CurrencyOprationType> CurrencyOprationTypes { get; set; }


    #endregion

    #region AuthDbSets

    public virtual DbSet<Form> Forms { get; set; }
    public DbSet<UserType> UserTypes { get; set; }
    public virtual DbSet<FormRole> FormRoles { get; set; }
    public virtual DbSet<FormGroup> FormGroups { get; set; }
    public virtual DbSet<FormUser> FormUsers { get; set; }
    public virtual DbSet<ActiveStatus> ActiveStatuses { get; set; }
    public virtual DbSet<AddressBook> AddressBooks { get; set; }
    public virtual DbSet<AddressType> AddressTypes { get; set; }
    public virtual DbSet<AdminLocationAccess> AdminLocationAccesses { get; set; }
    public virtual DbSet<Company> Companies { get; set; }
    public virtual DbSet<ConfigurationAttribute> ConfigurationAttributes { get; set; }
    public virtual DbSet<ConfigurationAttributeValue> ConfigurationAttributeValues { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Domain.Models.Features.Auths.Domains.Entities.Domain> Domains { get; set; }
    public virtual DbSet<Gender> Genders { get; set; }
    public virtual DbSet<Group> Groups { get; set; }
    public virtual DbSet<GroupPermission> GroupPermissions { get; set; }
    public virtual DbSet<Location> Locations { get; set; }
    public virtual DbSet<LocationType> LocationTypes { get; set; }
    public virtual DbSet<MainAggregate> MainAggregates { get; set; }
    public virtual DbSet<Permission> Permissions { get; set; }
    public virtual DbSet<PhoneType> PhoneTypes { get; set; }
    public virtual DbSet<PhoneBook> PhoneBooks { get; set; }
    public virtual DbSet<Position> Positions { get; set; }
    public virtual DbSet<ResponsibleType> ResponsibleTypes { get; set; }
    public virtual DbSet<Profile> Profiles { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<RolePermission> RolePermissions { get; set; }
    public virtual DbSet<Staff> Staff { get; set; }
    public virtual DbSet<SysConfig> SysConfigs { get; set; }
    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<UserConfig> UserConfigs { get; set; }
    public virtual DbSet<UserGroup> UserGroups { get; set; }
    public virtual DbSet<UserLocationAccess> UserLocationAccesses { get; set; }
    public virtual DbSet<UserPermission> UserPermissions { get; set; }
    public virtual DbSet<UserRole> UserRoles { get; set; }
    public virtual DbSet<ViewField> ViewFields { get; set; }
    public virtual DbSet<ViewList> ViewLists { get; set; }
    public virtual DbSet<UIInputElement> UIInputElements { get; set; }
    public virtual DbSet<ApiMethodAction> ApiMethodActions { get; set; }
    public virtual DbSet<PositionLevel> PositionLevels { get; set; }
    public virtual DbSet<PositionType> PositionTypes { get; set; }
    public virtual DbSet<AccessType> AccessTypes { get; set; }
    public virtual DbSet<TimeMeasurement> TimeMeasurements { get; set; }

    #endregion

    #region IssueDBSets

    public DbSet<IssueApproval> IssueApprovals { get; set; }
    public DbSet<IssueCustomFeild> IssueCustomFeilds { get; set; }
    public DbSet<IssueLinkReason> IssueLinkReasons { get; set; }
    public DbSet<IssuePriority> IssuePriorities { get; set; }
    public DbSet<Issue> Issues { get; set; }
    public DbSet<IssueType> IssueTypes { get; set; }
    public DbSet<IssueWeightCategory> IssueWeightCategories { get; set; }
    public DbSet<IssueChangeHistory> IssueChangeHistories { get; set; }
    public DbSet<IssueManager> IssueManagers { get; set; }

    #endregion

    #region DMSDbSets

    public DbSet<Document> Documents { get; set; }
    public DbSet<DocumentType> DocumentTypes { get; set; }
    public DbSet<DocumentExtension> DocumentExtensions { get; set; }
    public DbSet<WorkflowDocumentExtension> WorkflowDocumentExtensions { get; set; }
    public DbSet<WorkflowDocumentType> WorkflowDocumentTypes { get; set; }

    #endregion

    #region SecurityCommitees
    public DbSet<Approval> Approvals { get; set; }
    public DbSet<ApprovalResponsibleAnswer> ApprovalResponsibleAnswers { get; set; }
    public DbSet<ApprovalResponsibleAnswerDocument> ApprovalResponsibleAnswerDocuments { get; set; }
    public DbSet<ApprovalSupervisorAnswer> ApprovalSupervisorAnswers { get; set; }
    public DbSet<ApprovalSupervisorAnswerDocument> SupervisorAnswerDocuments { get; set; }
    public DbSet<Invitees> Invitees { get; set; }
    public DbSet<MeetingHoldingReason> MeetingHoldingReasons { get; set; }
    public DbSet<MeetingHoldingStatus> MeetingHoldingStatuses { get; set; }
    public DbSet<Meeting> Meetings { get; set; }
    public DbSet<MeetingDocument> MeetingDocuments { get; set; }
    public DbSet<MeetingReason> MeetingReasons { get; set; }
    public DbSet<MeetingSchedule> MeetingSchedules { get; set; }
    public DbSet<ResponsibleAnswerType> ResponsibleAnswerTypes { get; set; }
    public DbSet<SubjectPriority> SubjectPriorities { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<SubjectMeeting> SubjectMeetings { get; set; }
    public DbSet<SupervisorAnswerType> SupervisorAnswerTypes { get; set; }
    public DbSet<Label> Lables { get; set; }
    public DbSet<MeetingLabel> MeetingLabels { get; set; }
    #endregion

    #region RiskManagement
    public DbSet<RiskDegree> RiskDegrees { get; set; }
    public DbSet<MatrixAValue> MatrixAValues { get; set; }
    public DbSet<Frequency> Frequencies { get; set; }
    public DbSet<ScenarioHistory> ScenarioHistories { get; set; }
    public DbSet<Severity> Severities { get; set; }
    public DbSet<CurrentOccurrenceProbability> CurrentOccurrenceProbabilities { get; set; }
    public DbSet<InherentOccurrenceProbability> InherentOccurrenceProbabilities { get; set; }
    public DbSet<RiskLevelCobit> RiskLevelCobits { get; set; }
    public DbSet<CobitScenario> CobitScenarios { get; set; }
    public DbSet<MatrixA> MatrixAs { get; set; }
    public DbSet<UseVulnerability> UseVulnerabilities { get; set; }
    public DbSet<TriggerStatus> TriggerStatuses { get; set; }
    public DbSet<ConsequenceCategory> ConsequenceCategories { get; set; }
    public DbSet<CobitCategory> CobitCategories { get; set; }
    public DbSet<AffectedHistory> AffectedHistories { get; set; }
    public DbSet<SeverityValue> SeverityValues { get; set; }
    public DbSet<InherentOccurrenceProbabilityValue> InherentOccurrenceProbabilityValues { get; set; }
    public DbSet<CurrentOccurrenceProbabilityValue> CurrentOccurrenceProbabilityValues { get; set; }
    public DbSet<RiskValue> RiskValues { get; set; }
    public DbSet<ConsequenceLevel> ConsequenceLevels { get; set; }
    public DbSet<RiskLevel> RiskLevels { get; set; }
    public DbSet<RiskImpact> RiskImpacts { get; set; }
    public DbSet<RiskPossibility> RiskPossibilities { get; set; }
    public DbSet<RiskType> RiskTypes { get; set; }
    public DbSet<ImpactScale> ImpactScales { get; set; }
    public DbSet<RiskCriteria> RiskCriterias { get; set; }
    public DbSet<RiskLevelMeasure> RiskLevelMeasures { get; set; }
    public DbSet<Risk> Risks { get; set; }
    public DbSet<CorrectiveAction> CorrectiveActions { get; set; }
    public DbSet<PreventiveAction> PreventiveActions { get; set; }
    public DbSet<EffectedAsset> EffectedAssets { get; set; }
    public DbSet<RiskRelatedIssue> RiskRelatedIssues { get; set; }
    public DbSet<Vulnerability> vulnerabilities { get; set; }
    public DbSet<ServiceRiskImpact> ServiceRiskImpacts { get; set; }
    public DbSet<Threat> Threats { get; set; }
    public DbSet<ThreatType> ThreatTypes { get; set; }
    public DbSet<EvaluationCriteria> EvaluationCriterias { get; set; }
    #endregion

    #region ServiceCatalog
    public DbSet<ServiceCategory> ServiceCategories { get; set; }
    public DbSet<CustomerType> CustomerTypes { get; set; }
    public DbSet<CriticalActivity> CriticalActivities { get; set; }
    public DbSet<Product> Product { get; set; }
    public DbSet<ServiceStatus> ServiceStatuses { get; set; }
    public DbSet<ApiAuthenticationMethod> ApiAuthenticationMethods { get; set; }
    public DbSet<ApiType> ApiTypes { get; set; }
    public DbSet<ServiceType> ServiceTypes { get; set; }
    public DbSet<Channel> Channels { get; set; }
    public DbSet<Service> Services { get; set; }
    #endregion

    #region BCP
    public DbSet<ImportanceDegree> ImportanceDegrees { get; set; }
    public DbSet<OrganizationalServicePriority> OrganizationalServicePriorities { get; set; }
    public DbSet<SolutionPriority> SolutionPriorities { get; set; }
    public DbSet<ConsequenceIntension> ConsequenceIntensions { get; set; }
    public DbSet<RecoveryPointObjective> RecoveryPointObjectives { get; set; }
    public DbSet<HappeningPossibility> HappeningPossibilities { get; set; }
    public DbSet<Consequence> Consequences { get; set; }
    public DbSet<PlanResponsibility> PlanResponsibilities { get; set; }
    public DbSet<RecoveryOptionPriority> RecoveryOptionPriorities { get; set; }
    public DbSet<StrategyType> StrategyTypes { get; set; }
    public DbSet<BusinessContinuityStratgyResponsible> BusinessContinuityStratgyResponsibles { get; set; }
    public DbSet<ServicePriority> ServicePriorities { get; set; }
    public DbSet<BackupPeriod> BackupPeriods { get; set; }
    public DbSet<BusinessContinuityPlan> BusinessContinuityPlans { get; set; }
    public DbSet<Scenario> Scenarios { get; set; }
    public DbSet<ScenarioExecutionHistory> ScenarioExecutionHistories { get; set; }
    public DbSet<BusinessContinuityPlanPossibleAction> BusinessContinuityPlanPossibleActions { get; set; }
    public DbSet<ScenarioRecoveryOption> ScenarioRecoveryOptions { get; set; }
    public DbSet<ScenarioRecoveryCriteria> ScenarioRecoveryCriterias { get; set; }
    public DbSet<ScenarioBusinessContinuityPlanAssumption> ScenarioBusinessContinuityPlanAssumptions { get; set; }
    public DbSet<BusinessContinuityPlanVersioning> BusinessContinuityPlanVersionings { get; set; }
    public DbSet<BusinessContinuityStrategy> BusinessContinuityStrategies { get; set; }
    public DbSet<BusinessImpactAnalysis> BusinessImpactAnalysis { get; set; }
    public DbSet<BusinessImpactAnalysisDisasterOrigin> BusinessImpactAnalysisDisasterOrigins { get; set; }
    public DbSet<BusinessContinuityStrategyDocument> BusinessContinuityStrategyDocuments { get; set; }
    public DbSet<BusinessImpactAnalysisDocument> BusinessImpactAnalysisDocuments { get; set; }
    public DbSet<BusinessImpactAnalysisAsset> BusinessImpactAnalysisAssets { get; set; }
    public DbSet<BusinessImpactAnalysisCriticalActivity> BusinessImpactAnalysisCriticalActivities { get; set; }
    public DbSet<BusinessContinuityPlanCriticalActivity> BusinessContinuityPlanCriticalActivities { get; set; }
    public DbSet<BusinessContinuityStratgySolution> BusinessContinuityStratgySolutions { get; set; }
    public DbSet<BusinessContinuityStrategyObjective> BusinessContinuityStrategyObjectives { get; set; }
    public DbSet<BusinessContinuityPlanStratgy> BusinessContinuityPlanStratgies { get; set; }
    public DbSet<BusinessContinuityPlanService> BusinessContinuityPlanServices { get; set; }
    public DbSet<BusinessContinuityPlanRisk> BusinessContinuityPlanRisks { get; set; }
    public DbSet<BusinessContinuityPlanAssumption> BusinessContinuityPlanAssumptions { get; set; }
    public DbSet<ScenarioBusinessContinuityPlanVersioning> ScenarioBusinessContinuityPlanVersionings { get; set; }
    public DbSet<BusinessContinuityPlanRelatedStaff> BusinessContinuityPlanRelatedStaffs { get; set; }
    public DbSet<BusinessContinuityPlanResponsible> BusinessContinuityPlanResponsibles { get; set; }
    public DbSet<BusinessImpactAnalysisStaff> BusinessImpactAnalysisStaffs { get; set; }
    public DbSet<Origin> Origins { get; set; }
    #endregion

    #region Logistics
        public DbSet<UnitMeasurement> UnitMeasurements { get; set; }
        public DbSet<GoodsType> GoodsTypes { get; set; }
        public DbSet<GoodsCategory> GoodsCategories { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<GoodsStatus> GoodsStatus { get; set; }
        public DbSet<GoodsQuorumPrice> GoodsQuorumPrices { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierRank> SupplierRanks { get; set; }
        public DbSet<LogisticsRequest> LogisticsRequests { get; set; }
        public DbSet<LogisticsRequestGoods> LogisticsRequestGoods { get; set; }
        public DbSet<LogisticsSupply> LogisticsSupplies { get; set; }
        public DbSet<GoodsStatus> GoodsStatuses { get; set; }
    #endregion

    #region TrustyDrafts
    public DbSet<DraftOrigin> DraftOrigins { get; set; }
    public DbSet<WageDeductionMethod> WageDeductionMethods { get; set; }
    public DbSet<BrokerSecondLevelAddressBook> BrokerSecondLevelAddressBooks { get; set; }
    public DbSet<CurrencyPaymentChannel> CurrencyPaymentChannels { get; set; }
    public DbSet<WageRate> WageRates { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<TrustyDraft> TrustyDrafts { get; set; }
    public DbSet<RequestValor> RequestValors { get; set; }
    public DbSet<ResponsibilityWageType> ResponsibilityWageTypes { get; set; }
    public DbSet<DraftReviewResult> DraftReviewResults { get; set; }
    public DbSet<CancellationResaon> CancellationResaons { get; set; }
    public DbSet<DraftCurrencyOrigin> DraftCurrencyOrigins { get; set; }
    public DbSet<DraftType> DraftTypes { get; set; }
    public DbSet<ReconsilationType> ReconsilationTypes { get; set; }
    public DbSet<DraftIssueType> DraftIssueTypes { get; set; }
    public DbSet<DraftValorStatus> DraftValorStatuses { get; set; }
    public DbSet<DraftStatus> DraftStatuses { get; set; }
    public DbSet<DraftDestination> DraftDestinations { get; set; }
    public DbSet<BrokerInquiryStatus> BrokerInquiryStatuses { get; set; }
    public DbSet<AgentBankWageShareStatus> AgentBankWageShareStatuses { get; set; }
    public DbSet<InquiryResponse> InquiryResponses { get; set; }
    public DbSet<InquiryRequest> InquiryRequests { get; set; }
    public DbSet<ReferralLetter> ReferralLetters { get; set; }
    #endregion

    #region AssetAndConfigurations
    public DbSet<AssetPhysicalStatus> AssetPhysicalStatuses { get; set; }
    public DbSet<AssetTechnicalStatus> AssetTechnicalStatuses { get; set; }
    public DbSet<BusinessCriticality> BusinessCriticalities { get; set; }
    public DbSet<ConfigurationItemStatus> ConfigurationItemStatuses { get; set; }
    public DbSet<AssetType> AssetTypes { get; set; }
    public DbSet<ConfigurationItemType> ConfigurationItemTypes { get; set; }
    public DbSet<ConfigurationItemRelationshipType> ConfigurationItemRelationshipTypes { get; set; }
    public DbSet<LicenseType> LicenseTypes { get; set; }
    #endregion

    #region Notifications

    public DbSet<Message> Messages { get; set; }

    #endregion
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { 
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkFlowConfiguration).Assembly);
    }
}
