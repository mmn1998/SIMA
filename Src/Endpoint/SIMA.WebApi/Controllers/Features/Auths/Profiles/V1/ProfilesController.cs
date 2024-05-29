using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SIMA.Application.Contract.Features.Auths.Profiles;
using SIMA.Framework.Common.Response;

namespace SIMA.WebApi.Controllers.Features.Auths.Profiles.V1
{
    [Route("[controller]")]
    [ApiController]
    [ApiExplorerSettings(GroupName = "Profiles")]
    [Authorize]

    public class ProfilesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProfilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<Result> Post([FromBody] CreateProfileCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Result> Delete(long id)
        {
            var command = new DeleteProfileCommand { Id = id };
            return await _mediator.Send(command);
        }

        [HttpPost("AddressBook")]
        public async Task<Result> Post(CreateAddressBookCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut("AddressBook")]
        public async Task<Result> Post(ModifyAddressBookCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpDelete("{profileId}/{addressBookId}")]
        public async Task<Result> Delete([FromRoute] int profileId, [FromRoute] int addressBookId)
        {
            var command = new RemoveAddressBookCommand { ProfileId = profileId, Id = addressBookId };
            return await _mediator.Send(command);
        }

        [HttpPost("PhoneBook")]
        public async Task<Result> Post(CreatePhoneBookCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

        [HttpPut("PhoneBook")]
        public async Task<Result> Post(ModifyPhoneBookCommand command)
        {
            var result = await _mediator.Send(command);
            return result;
        }

    }
}
