using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.WorkFlowEngine.BPMSes;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;

namespace SIMA.WebApi.Controllers.Features.WorkFlowEngine.BPMSes;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[ApiExplorerSettings(GroupName = "BPMS")]
public class BPMSController : ControllerBase
{
    private readonly IMediator _mediator;

    public BPMSController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [SimaAuthorize(Permissions.BMPSPost)]
    public async Task<Result> Post(CreateBpmsCommand command)
    {
        return await _mediator.Send(command);
    }
    [HttpPut]
    [SimaAuthorize(Permissions.BMPSPost)]
    public async Task<Result> Put(ModifyBpmsCommand command)
    {
        return await _mediator.Send(command);
    }
}

