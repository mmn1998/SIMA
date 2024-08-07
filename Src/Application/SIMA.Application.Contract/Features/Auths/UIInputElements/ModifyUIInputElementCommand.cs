using SIMA.Framework.Common.Response;
using Sima.Framework.Core.Mediator;

namespace SIMA.Application.Contract.Features.Auths.UIInputElements;

public class ModifyUIInputElementCommand : ICommand<Result<long>>
{
    public long Id { get; set; }
    public string? Name { get; set; }
    public string? Code { get; set; }
    public string? IsMultiSelect { get; set; }
    public string? IsSingleSelect { get; set; }
    public string? HasInputInEachRecord { get; set; }
}
