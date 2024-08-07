using SIMA.Framework.Common.Helper;

namespace SIMA.Application.Query.Contract.Features.Auths.Profiles;

public class GetProfileQueryResult
{
    public long Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? FatherName { get; set; }
    public string? NationalCode { get; set; }
    public long ActiveStatusId { get; set; }
    public string? ActiveStatus { get; set; }
    public long GenderId { get; set; }
    public string? GenderName { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? BirthDatePersian => BirthDate.ToPersianDate();
}
