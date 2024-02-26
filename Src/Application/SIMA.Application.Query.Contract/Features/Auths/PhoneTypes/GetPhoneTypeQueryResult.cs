namespace SIMA.Application.Query.Contract.Features.Auths.PhoneTypes;

public class GetPhoneTypeQueryResult
{
    public long Id { get; set; }

    public string? Name { get; set; }

    public string? Code { get; set; }

    public long ActiveStatusId { get; set; }
    public string ActiveStatus { get; set; }

    public byte[]? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public int? ModifyBy { get; set; }
}
