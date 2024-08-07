using Sima.Framework.Core.Mediator;
using SIMA.Framework.Common.Response;

namespace SIMA.Application.Contract.Features.Auths.UIInputElements;

public class CreateUIInputElementCommand : ICommand<Result<long>>
{
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsMultiSelect { get; set; }
    public string? IsSingleSelect { get; set; }
    public string? HasInputInEachRecord { get; set; }
}