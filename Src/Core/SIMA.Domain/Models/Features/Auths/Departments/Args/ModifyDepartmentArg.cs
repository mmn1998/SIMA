namespace SIMA.Domain.Models.Features.Auths.Departments.Args
{
    public class ModifyDepartmentArg
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public long? ParentId { get; set; }
        public long? CompanyId { get; set; }
        public byte[]? ModifiedAt { get; set; }
        public long? ModifiedBy { get; set; }
        public long ActiveStatusId { get; set; }

    }
}
