namespace SIMA.Application.Query.Contract.Features.Auths.ConfigurationAttributes;

public class GetConfigurationAttributeQueryResult
{
    public long Id { get; set; }

    public string? EnglishKey { get; set; }

    public string? Name { get; set; }

    public string? IsUserConfige { get; set; }

    public long ActiveStatusId { get; set; }

    public byte[]? CreateDate { get; set; }

    public int? CreateBy { get; set; }

    public DateTime? ModifyDate { get; set; }

    public int? ModifyBy { get; set; }
}
