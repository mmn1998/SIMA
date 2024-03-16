using AutoMapper;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Profiles;
using SIMA.Domain.Models.Features.Auths.Profiles.Args;
using SIMA.Domain.Models.Features.Auths.Profiles.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Core.Mediator;

namespace SIMA.Application.Feaatures.Auths.Profiles;

public class ProfileCommandHandler :
    ICommandHandler<CreateProfileCommand, Result<long>>,
    ICommandHandler<DeleteProfileCommand, Result<long>>,
    ICommandHandler<CreateAddressBookCommand, Result<long>>,
    ICommandHandler<CreatePhoneBookCommand, Result<long>>,
    ICommandHandler<ModifyAddressBookCommand, Result<long>>,
    ICommandHandler<RemoveAddressBookCommand, Result<long>>
{
    private readonly IMapper _mapper;
    private readonly IProfileRepository _repository;
    private readonly IProfileService _profileService;
    private readonly IUnitOfWork _unitOfWork;

    public ProfileCommandHandler(IMapper mapper, IProfileRepository repository,
         IProfileService profileService, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _repository = repository;
        _profileService = profileService;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<long>> Handle(CreateProfileCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateProfileArg>(request);
        var entity = await Domain.Models.Features.Auths.Profiles.Entities.Profile.Create(_profileService, arg);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteProfileCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateAddressBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.ProfileId);
        var addressBookArg = _mapper.Map<CreateAddressBookArg>(request);
        await entity.AddAddressBook(_profileService, addressBookArg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.ProfileId);
    }

    public async Task<Result<long>> Handle(ModifyAddressBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.ProfileId);
        var addressBookArg = _mapper.Map<ModifyAddressBookArg>(request);
        await entity.ModifyAddressBook(addressBookArg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(RemoveAddressBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.ProfileId);
        entity.RemoveAddressBook(request.Id);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.ProfileId);
    }

    public async Task<Result<long>> Handle(CreatePhoneBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.ProfileId);
        var phoneBookArg = _mapper.Map<CreatePhoneBookArg>(request);
        await entity.AddPhobeBook(phoneBookArg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(ModifyPhoneBookCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.ProfileId);
        var phoneBookArg = _mapper.Map<ModifyPhoneBookArg>(request);
        await entity.ModifyPhoneBook(phoneBookArg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(request.ProfileId);
    }
}
