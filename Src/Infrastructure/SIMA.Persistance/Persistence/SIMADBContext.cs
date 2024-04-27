using Microsoft.EntityFrameworkCore;
using SIMA.Domain.Models.Features.Auths.ActiveStatuses.Entities;
using SIMA.Domain.Models.Features.Auths.AddressTypes.Entities;
using SIMA.Domain.Models.Features.Auths.AdminLocationAccesses.Entities;
using SIMA.Domain.Models.Features.Auths.Companies.Entities;
using SIMA.Domain.Models.Features.Auths.ConfigurationAttributes.Entities;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;
using SIMA.Domain.Models.Features.Auths.Forms.Entities;
using SIMA.Domain.Models.Features.Auths.Genders.Entities;
using SIMA.Domain.Models.Features.Auths.Groups.Entities;
using SIMA.Domain.Models.Features.Auths.Locations.Entities;
using SIMA.Domain.Models.Features.Auths.LocationTypes.Entities;
using SIMA.Domain.Models.Features.Auths.MainAggregates.Entities;
using SIMA.Domain.Models.Features.Auths.Permissions.Entities;
using SIMA.Domain.Models.Features.Auths.PhoneTypes.Entities;
using SIMA.Domain.Models.Features.Auths.Positions.Entities;
using SIMA.Domain.Models.Features.Auths.Profiles.Entities;
using SIMA.Domain.Models.Features.Auths.Roles.Entities;
using SIMA.Domain.Models.Features.Auths.Staffs.Entities;
using SIMA.Domain.Models.Features.Auths.SysConfigs.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BranchTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.Brokers.Entities;
using SIMA.Domain.Models.Features.BranchManagement.BrokerTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.CurrencyTypes.Entities;
using SIMA.Domain.Models.Features.BranchManagement.PaymentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentExtensions.Entities;
using SIMA.Domain.Models.Features.DMS.Documents.Entities;
using SIMA.Domain.Models.Features.DMS.DocumentTypes.Entities;
using SIMA.Domain.Models.Features.DMS.WorkflowDocumentExtensions.Entities;
using SIMA.Domain.Models.Features.DMS.WorkFlowDocumentTypes.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueApprovals.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueCustomFeilds.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueLinkReasons.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssuePriorities.Entities;
using SIMA.Domain.Models.Features.IssueManagement.Issues.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueTypes.Entities;
using SIMA.Domain.Models.Features.IssueManagement.IssueWeightCategories.Entities;
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
using SIMA.Domain.Models.Features.WorkFlowEngine.ActionType.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.Progress.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.Project.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlow.Entities;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowActor.Entites;
using SIMA.Domain.Models.Features.WorkFlowEngine.WorkFlowCompany.Entities;
using SIMA.Persistance.EntityConfigurations.Features.WorkFlowEngine.WorkFlowConfiguration;

namespace SIMA.Persistance.Persistence
{
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
        #endregion

        #region BranchDbSets

        public virtual DbSet<SIMA.Domain.Models.Features.BranchManagement.Branches.Entities.Branch> Branches { get; set; }

        public virtual DbSet<BranchType> BranchTypes { get; set; }

        public virtual DbSet<Broker> Brokers { get; set; }

        public virtual DbSet<BrokerType> BrokerTypes { get; set; }

        public virtual DbSet<CurrencyType> CurrencyTypes { get; set; }

        public virtual DbSet<PaymentType> PaymentTypes { get; set; }


        #endregion

        #region AuthDbSets

        public virtual DbSet<Form> Forms { get; set; }
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

        public virtual DbSet<MainAggregate> MainEntities { get; set; }

        public virtual DbSet<Permission> Permissions { get; set; }

        public virtual DbSet<PhoneType> PhoneTypes { get; set; }

        public virtual DbSet<PhoneBook> PhoneBooks { get; set; }

        public virtual DbSet<Position> Positions { get; set; }

        public virtual DbSet<Profile> Profiles { get; set; }
        public virtual DbSet<Role> Roles { get; set; }

        public virtual DbSet<RolePermission> RolePermissions { get; set; }

        public virtual DbSet<Staff> Staff { get; set; }

        public virtual DbSet<SysConfig> SysConfigs { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<UserConfig> UserConfigs { get; set; }

        public virtual DbSet<UserDomainAccess> UserDomainAccesses { get; set; }

        public virtual DbSet<UserGroup> UserGroups { get; set; }

        public virtual DbSet<UserLocationAccess> UserLocationAccesses { get; set; }

        public virtual DbSet<UserPermission> UserPermissions { get; set; }

        public virtual DbSet<UserRole> UserRoles { get; set; }

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
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(WorkFlowConfiguration).Assembly);



        }
    }
}
