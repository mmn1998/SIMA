using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.ReferralLetters;

public class ModifyReferralLetterCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? LetterNumber { get; set; }
    public string? LetterDate { get; set; }
    public long BrokerId { get; set; }
    public long? LetterDocumentId { get; set; }
}
