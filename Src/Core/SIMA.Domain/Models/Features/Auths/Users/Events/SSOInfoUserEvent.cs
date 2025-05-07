namespace SIMA.Domain.Models.Features.Auths.Users.Events
{
    public class SSOInfoUserEvent
    {
        public string EmployeeCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UnitCode { get; set; }
        public string UnitName { get; set; }
        public string ParentUnitCode { get; set; }
        public string ParentUnitName { get; set; }
        public string JobTitleCode { get; set; }
        public string JobTitleName { get; set; }
        public string JobStatusName { get; set; }
    }
}
