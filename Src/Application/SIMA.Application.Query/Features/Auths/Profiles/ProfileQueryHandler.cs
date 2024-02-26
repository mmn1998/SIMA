using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Query.Contract.Features.Auths.Profiles;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Profiles;

namespace SIMA.Application.Query.Features.Auths.Profiles;

public class ProfileQueryHandler : IQueryHandler<GetProfileQuery, Result<GetProfileQueryResult>>,
    IQueryHandler<GetAllProfileQuery, Result<List<GetProfileQueryResult>>>,
    IQueryHandler<GetAllShortProfileQuery, Result<List<GetShortProfileQueryResult>>>,
    IQueryHandler<GetManagersByCompanyId, Result<List<SelectModel>>>,
    IQueryHandler<GetAllPhoneBookQuery, Result<List<GetPhoneBookQueryResult>>>,
    IQueryHandler<GetAllAddressBookQuery, Result<List<GetAddressBookQueryResult>>>
{
    private readonly IMapper _mapper;
    private readonly IProfileQueryRepository _repository;

    public ProfileQueryHandler(IMapper mapper, IProfileQueryRepository repository)
    {
        _mapper = mapper;
        _repository = repository;
    }

    public async Task<Result<GetProfileQueryResult>> Handle(GetProfileQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.FindById(request.Id);
        return result;
    }

    public async Task<Result<List<GetShortProfileQueryResult>>> Handle(GetAllShortProfileQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetShort();
        return Result.Ok(entity);
    }

    public async Task<Result<List<GetProfileQueryResult>>> Handle(GetAllProfileQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAll(request.Request);
        //var entity = await _repository.GetAll(request.Request);
        //return Result.Ok(entity);
    }

    public async Task<Result<List<GetPhoneBookQueryResult>>> Handle(GetAllPhoneBookQuery request, CancellationToken cancellationToken)
    {
        return await _repository.GetAllPhoneBooks((int)request.Id, request.Request);

    }

    public async Task<Result<List<GetAddressBookQueryResult>>> Handle(GetAllAddressBookQuery request, CancellationToken cancellationToken)
    {
        return await _repository.FindWithAddressBook(request.Id, request.Request);
    }

    public async Task<Result<List<SelectModel>>> Handle(GetManagersByCompanyId request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetMangersByCompanyId(request.Id);
        return Result.Ok(result);
    }


}
