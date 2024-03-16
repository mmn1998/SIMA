using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Users;
using SIMA.Application.Feaatures.Auths.Users.Mappers;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Domain.Models.Features.Auths.Profiles.Args;
using SIMA.Domain.Models.Features.Auths.Profiles.Interfaces;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Interfaces;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Core.Mediator;
using SIMA.Persistance.Read.Repositories.Features.Auths.Users;

namespace SIMA.Application.Feaatures.Auths.Users;

public class UserCommandHandler : ICommandHandler<DeleteUserCommand, Result<long>>, ICommandHandler<CreateUserCommand, Result<long>>,
    ICommandHandler<CreateUserRoleCommand, Result<long>>
    , ICommandHandler<CreateUserPermissionCommand, Result<long>>, ICommandHandler<CreateUserDomainCommand, Result<long>>,
    ICommandHandler<CreateUserLocationCommand, Result<long>>,
    ICommandHandler<UpdateUserCommand, Result<long>>, ICommandHandler<UpdateUserRoleCommand, Result<long>>,
    ICommandHandler<UpdateUserPermissionCommand, Result<long>>
    , ICommandHandler<UpdateUserLocationCommand, Result<long>>, ICommandHandler<UpdateUserDomainCommand,
        Result<long>>, ICommandHandler<DeleteUserDomainCommand, Result<long>>
    , ICommandHandler<DeleteUserLocationCommand, Result<long>>, ICommandHandler<DeleteUserPermissionCommand, Result<long>>,
    ICommandHandler<DeleteUserRoleCommand, Result<long>>, ICommandHandler<CreateUserAggregateCommand, Result<long>>,
    ICommandHandler<GetUserNameWithSSO, Result<LoginUserQueryResult>>


{
    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _service;

    //ForSSO
    private readonly IProfileService _profileService;
    private readonly IProfileRepository _profileRepository;
    private readonly IUserQueryRepository _queryrepository;
    private readonly TokenModel _securitySettings;

    public UserCommandHandler(IMapper mapper, IUserRepository repository,
         IUnitOfWork unitOfWork, IUserService service, ILogger<UserCommandHandler> logger, IProfileRepository profileRepository,
         IProfileService profileService, IUserQueryRepository queryrepository, IOptions<TokenModel> securitySettings)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
        _profileService = profileService;
        _profileRepository = profileRepository;
        _queryrepository = queryrepository;
        _securitySettings = securitySettings.Value;
    }
    public async Task<Result<long>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById((int)request.Id);
        entity.Delete();
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateUserArg>(request);
        var entity = await User.Create(_service, arg);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        var arg = _mapper.Map<CreateUserRoleArg>(request);
        await entity.AddUserRole(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateUserPermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        var arg = _mapper.Map<CreateUserPermissionArg>(request);
        await entity.AddUserPermission(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateUserDomainCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        var arg = _mapper.Map<CreateUserDomainArg>(request);
        await entity.AddUserDomain(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateUserLocationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        var arg = _mapper.Map<CreateUserLocationAccessArg>(request);
        await entity.AddUserLocation(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

        public async Task<Result<long>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.Id);
            var arg = _mapper.Map<ModifyUserArg>(request);
            await entity.Modify(arg, _service);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.UserId);
            var arg = _mapper.Map<ModifyUserRoleArg>(request);
            await entity.ModifyUserRole(arg);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(UpdateUserPermissionCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.UserId);
            var arg = _mapper.Map<ModifyUserPermissionArg>(request);
            await entity.ModifyUserPermission(arg);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

        public async Task<Result<long>> Handle(UpdateUserDomainCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.UserId);
            var arg = _mapper.Map<ModifyUserDomainArg>(request);
            await entity.ModifyUserDomain(arg);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

    //public async Task<int> Handle(UpdateUserGroupCommand request, CancellationToken cancellationToken)
    //{
    //    var user = await _queryRepository.FindById((long)request.UserId);
    //    var arg = _mapper.Map<ModifyUserGroupArg>(request);
    //    user.ModifyUserGroup(arg);
    //    await _unitOfWork.SaveChangesAsync();
    //    return user.Id;
    //}

        public async Task<Result<long>> Handle(UpdateUserLocationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _repository.GetById(request.UserId);
            var arg = _mapper.Map<ModifyUserLocationArg>(request);
            await entity.ModifyUserLocation(arg);
            await _unitOfWork.SaveChangesAsync();
            return Result.Ok(entity.Id.Value);
        }

    public async Task<Result<long>> Handle(DeleteUserDomainCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        entity.DeleteUserDomainAccess(request.UserDomainAccessId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(DeleteUserLocationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        entity.DeleteUserLocationAccess(request.UserLocationAccessId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(DeleteUserPermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        entity.DeleteUserPermission(request.UserPermissionId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        entity.DeleteUserRole(request.UserRoleId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(CreateUserAggregateCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateUserArg>(request.User);
        var entity = await User.Create(_service, arg);
        await _repository.Add(entity);
        if (request.UserDomains != null && request.UserDomains.Count != 0)
        {
            var userDomainsArgs = _mapper.Map<List<CreateUserDomainArg>>(request.UserDomains);
            foreach (var domain in userDomainsArgs) domain.UserId = entity.Id.Value;
            await entity.AddUserDomains(userDomainsArgs);
        }
        if (request.UserPermissions != null && request.UserPermissions.Count != 0)
        {
            var userPermissionsArgs = _mapper.Map<List<CreateUserPermissionArg>>(request.UserPermissions);
            foreach (var permission in userPermissionsArgs) permission.UserId = entity.Id.Value;
            await entity.AddUserPermissions(userPermissionsArgs);
        }

        if (request.UserRoles != null && request.UserRoles.Count != 0)
        {
            var userRolesArgs = _mapper.Map<List<CreateUserRoleArg>>(request.UserRoles);
            foreach (var role in userRolesArgs) role.UserId = entity.Id.Value;
            await entity.AddUserRoles(userRolesArgs);
        }
        if (request.UserLocations != null && request.UserLocations.Count != 0)
        {
            var userLocationsArgs = _mapper.Map<List<CreateUserLocationAccessArg>>(request.UserLocations);
            foreach (var location in userLocationsArgs) location.UserId = entity.Id.Value;
            await entity.AddUserLocations(userLocationsArgs);
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    //ForSSO
    public async Task<Result<LoginUserQueryResult>> Handle(GetUserNameWithSSO request, CancellationToken cancellationToken)
    {
        var userSSO = await _repository.GetUserInfoWithSSO(request.Tiket);

        var user = await _repository.GetByUserName(userSSO.EmployeeCode);
        if (user is null)
        {
            #region Profile

            var profileArg = new CreateProfileArg();
            profileArg.ActiveStatusId = 1;
            profileArg.CreatedAt = DateTime.Now;
            profileArg.FirstName = userSSO.FirstName;
            profileArg.LastName = userSSO.LastName;
            profileArg.FatherName = "Test";
            profileArg.NationalId = "3587119511";
            var profileEntity = await Domain.Models.Features.Auths.Profiles.Entities.Profile.Create(_profileService, profileArg);
            await _profileRepository.Add(profileEntity);
            await _unitOfWork.SaveChangesAsync();

            #endregion

            #region user
            var profileId = profileEntity.Id;
            CreateUserCommand usercommand = new CreateUserCommand();
            usercommand.Username = userSSO.EmployeeCode;
            usercommand.CompanyId = 3;
            usercommand.ProfileId = Convert.ToInt64(profileId);
            usercommand.Password = userSSO.EmployeeCode;
            var arg = _mapper.Map<CreateUserArg>(usercommand);
            var userEntity = await User.Create(_service, arg);
            await _repository.Add(userEntity);
            await _unitOfWork.SaveChangesAsync();

            #endregion

            #region Permission

            CreateUserPermissionArg permissionArg = new CreateUserPermissionArg();
            permissionArg.UserId = Convert.ToInt64(userEntity.Id);
            permissionArg.PermissionId = 248;
            permissionArg.ActiveStatusId = 1;
            permissionArg.CreatedAt = DateTime.Now;
            await userEntity.AddUserPermission(permissionArg);
            await _unitOfWork.SaveChangesAsync();

            #endregion

            var tokenUser = await _queryrepository.GetByUsernameAndPassword(userEntity.Username, userSSO.EmployeeCode);
            var result = UserMapper.MapToToken(tokenUser, _securitySettings);
            return Result.Ok(result);
        }
        else
        {
            //TODO Change GetByUsernameAndPassword
            var tokenUser = await _queryrepository.GetByUsernameAndPassword(user.Username, userSSO.EmployeeCode);
            var result = UserMapper.MapToToken(tokenUser, _securitySettings);
            return Result.Ok(result);
        }
    }
}
