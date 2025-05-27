using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Auths.CustomeFieldTypes
{
    public class GetCustomeFieldTypeQueryResult
    {
        public long Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? IsList { get; set; }
        public string? IsMultiSelect { get; set; }
        public long ActiveStatusId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? PersianCreatedAt => DateHelper.ToPersianDate(CreatedAt);
        public string? ActiveStatus { get; set; }
    }
}
