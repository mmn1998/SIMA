using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.TrustyDrafts.ReferralLetters;

public class CreateReferralLetterCommand : ICommand<Result<CreateReferralLetterResult>>
{
    public long BrokerId { get; set; }
    public long? LetterDocumentId { get; set; }
    public long TrustyDraftId { get; set; }
}
