namespace SIMA.Application.Query.Contract.Features.Notifications.Messages
{
    public class GetNotificationMessagePositionDisplay
    {
        public long PositionId { get; set; }
        public long PositionTypeId { get; set; }
        public string? PositionTypeName { get; set; }
        public long PositionLevelId { get; set; }
        public string? PositionLevelName { get; set; }
        public long DepartmentId { get; set; }
        public string? DepartmentName { get; set; }
        public long BranchId { get; set; }
        public string? BranchName { get; set; }
        public string? ActiveStatus { get; set; }
        public string? CreatedAt { get; set; }
    }
}

