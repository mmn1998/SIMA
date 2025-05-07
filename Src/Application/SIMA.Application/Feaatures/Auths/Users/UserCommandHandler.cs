using AutoMapper;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Sima.Framework.Core.Repository;
using SIMA.Application.Contract.Features.Auths.Users;
using SIMA.Application.Feaatures.Auths.Users.Mappers;
using SIMA.Application.Query.Contract.Features.Auths.Users;
using SIMA.Domain.Models.Features.Auths.Groups.Args;
using SIMA.Application.Services;
using SIMA.Application.Services.Request;
using SIMA.Domain.Models.Features.Auths.Profiles.Args;
using SIMA.Domain.Models.Features.Auths.Profiles.Interfaces;
using SIMA.Domain.Models.Features.Auths.Users.Args;
using SIMA.Domain.Models.Features.Auths.Users.Entities;
using SIMA.Domain.Models.Features.Auths.Users.Interfaces;
using SIMA.Framework.Common.Exceptions;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Response;
using SIMA.Framework.Common.Security;
using SIMA.Framework.Common.Services;
using SIMA.Framework.Core.Mediator;
using SIMA.Framework.Infrastructure.Cachings;
using SIMA.Persistance.Read.Repositories.Features.Auths.Users;
using SIMA.Resources;

namespace SIMA.Application.Feaatures.Auths.Users;

public class UserCommandHandler : ICommandHandler<DeleteUserCommand, Result<long>>,
    ICommandHandler<CreateUserCommand, Result<long>>,
    ICommandHandler<UpdateUserCommand, Result<long>>, 
    ICommandHandler<UpdateUserRoleCommand, Result<long>>,
    ICommandHandler<UpdateUserPermissionCommand, Result<long>>
    , ICommandHandler<UpdateUserLocationCommand, Result<long>>
    , ICommandHandler<DeleteUserLocationCommand, Result<long>>,
    ICommandHandler<DeleteUserPermissionCommand, Result<long>>,
    ICommandHandler<DeleteUserRoleCommand, Result<long>>,
    ICommandHandler<CreateUserAggregateCommand, Result<long>>,
    ICommandHandler<GetUserNameWithSSO, Result<LoginUserQueryResult>>,
    ICommandHandler<ChangePasswordCommand, Result<long>>,
    ICommandHandler<CheckUserCommand, Result<long>>,
    ICommandHandler<ConfirmCodeCommand, Result<long>>,
    IQueryHandler<LoginUserQuery, Result<LoginUserQueryResult>>,
    IQueryHandler<GetConfirmOTPCode, Result<LoginUserQueryResult>>,
    ICommandHandler<ReSendOTPCommand, Result<long>>,
    IQueryHandler<SendOTPByUsernameCommand, Result<LoginUserQueryResult>>


{

    private readonly IMapper _mapper;
    private readonly IUserRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserService _service;
    private readonly ITokenService _tokenService;
    private readonly ISMSService _sMSService;

    //ForSSO
    private readonly IProfileService _profileService;
    private readonly IProfileRepository _profileRepository;
    private readonly IUserQueryRepository _queryrepository;
    private readonly ISimaIdentity _simaIdentity;
    private readonly TokenModel _securitySettings;
    private readonly IDistributedRedisService _redisService;

    public UserCommandHandler(IMapper mapper, IUserRepository repository,
         IUnitOfWork unitOfWork, IUserService service, ILogger<UserCommandHandler> logger, IProfileRepository profileRepository,
         IProfileService profileService, IUserQueryRepository queryrepository, IOptions<TokenModel> securitySettings,
         ISimaIdentity simaIdentity, IDistributedRedisService redisService, ITokenService tokenService, ISMSService sMSService)
    {
        _mapper = mapper;
        _repository = repository;
        _unitOfWork = unitOfWork;
        _service = service;
        _profileService = profileService;
        _profileRepository = profileRepository;
        _queryrepository = queryrepository;
        _simaIdentity = simaIdentity;
        _securitySettings = securitySettings.Value;
        _redisService = redisService;
        _tokenService = tokenService;
        _sMSService = sMSService;
    }

    public async Task<Result<LoginUserQueryResult>> Handle(LoginUserQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetByUsernameAndPassword(request.Username, request.Password);

            if (user.IsFirstLogin == "0" && user.IsSendOTP == "1")
            {
                var code = await user.GenerateCode(_service);

                #region Send SMS

                var userMobileNumber = await _queryrepository.GetUserMobileNumber(user.Id.Value);

                var sendsms = await _sMSService.SendSMS(new SendSMSRequest
                {
                    DestinationAddress = userMobileNumber,
                    MessageText = $"کد فعال سازی جهت ورود به سیستم {code}"
                });

                if (!sendsms.Succeeded)
                    throw new SimaResultException(CodeMessges._400Code, Messages.FailedSendSMS);

                #endregion

                #region Expirtion Code
                string key = RedisKeys.SendSMS + user.Id.Value;
                TimeSpan expirtionTime = TimeSpan.FromMinutes(3);
                _redisService.Delete(key);
                await _redisService.InsertAsync(key, code, expirtionTime);
                #endregion
            }
            await _unitOfWork.SaveChangesAsync();

            if (string.Equals(user.IsLocked, "0") && user.AccessFailedCount < 4 && user.AccessFailedCount > 0)
                throw new SimaResultException(CodeMessges._400Code, Messages.InvalidUsernameOrPasswordError);
            

            else if (string.Equals(user.IsLocked, "1"))
                throw new SimaResultException(CodeMessges._400Code, Messages.UserIsLocked);

            var permissions = await _queryrepository.GetPermissions(user.Id.Value);
            permissions.UserInfoLogin.ConfirmCode = null;

            var result = UserMapper.MapToToken(permissions, _tokenService);

            //insert refreshToken in Redis
#if !DEBUG
            await _redisService.InsertAsync(permissions.UserInfoLogin.Username, result.RefreshToken, TimeSpan.FromHours(_securitySettings.RefreshTokenLifeTime));
#endif
             return Result.Ok(result);
        }
        catch (Exception ex)
        {
            throw;
        }

    }
    public async Task<Result<long>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.Id);
        long userId = _simaIdentity.UserId; entity.Delete(userId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateUserArg>(request);
        arg.CreatedBy = _simaIdentity.UserId;
        var entity = await User.Create(_service, arg);
        await _repository.Add(entity);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }

    public async Task<Result<long>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        try 
        {
             var entity = await _repository.GetById(request.Id);
        var arg = _mapper.Map<ModifyUserArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.Modify(arg, _service);

        if (request.FormUsers != null && request.FormUsers.Count != 0)
        {
            var formUserArg = _mapper.Map<List<CreateFormUserArg>>(request.FormUsers);
            foreach (var item in formUserArg)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.UserId = entity.Id.Value;
            }

            await entity.AddFormUser(formUserArg, entity.Id.Value, _service);
        }
        // todo: sanaz for test
        if (request.UserPermissions != null && request.UserPermissions.Count != 0)
        {
            var userPermissionsArgs = _mapper.Map<List<CreateUserPermissionArg>>(request.UserPermissions);
            foreach (var permission in userPermissionsArgs) permission.UserId = entity.Id.Value;
            foreach (var item in userPermissionsArgs) item.CreatedBy = _simaIdentity.UserId;
            await entity.AddUserPermission(userPermissionsArgs, entity.Id.Value);
        }

        if (request.UserRoles != null && request.UserRoles.Count != 0)
        {
            var userRolesArgs = _mapper.Map<List<CreateUserRoleArg>>(request.UserRoles);
            foreach (var role in userRolesArgs) role.UserId = entity.Id.Value;
            foreach (var item in userRolesArgs) item.CreatedBy = _simaIdentity.UserId;
            await entity.AddUserRole(userRolesArgs, entity.Id.Value);
        }

        if (request.UserGroups != null && request.UserGroups.Count != 0)
        {
            var userGroupsArgs = _mapper.Map<List<CreateUserGroupArg>>(request.UserGroups);
            foreach (var role in userGroupsArgs) role.UserId = entity.Id.Value;
            foreach (var item in userGroupsArgs) item.CreatedBy = _simaIdentity.UserId;
            await entity.AddUserGroup(userGroupsArgs, entity.Id.Value);
        }

        if (request.UserLocations != null && request.UserLocations.Count != 0)
        {
            var userLocationsArgs = _mapper.Map<List<CreateUserLocationAccessArg>>(request.UserLocations);
            foreach (var location in userLocationsArgs) location.UserId = entity.Id.Value;
            foreach (var item in userLocationsArgs) item.CreatedBy = _simaIdentity.UserId;
            await entity.AddUserLocation(userLocationsArgs, entity.Id.Value);
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
        }
        catch(Exception ex)
        {
            throw;
        }
       
    }

    public async Task<Result<long>> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        var arg = _mapper.Map<ModifyUserRoleArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.ModifyUserRole(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(UpdateUserPermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        var arg = _mapper.Map<ModifyUserPermissionArg>(request);
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.ModifyUserPermission(arg);
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
        arg.ModifiedBy = _simaIdentity.UserId;
        await entity.ModifyUserLocation(arg);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteUserLocationCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        entity.DeleteUserLocationAccess(request.UserLocationAccessId, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteUserPermissionCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        entity.DeleteUserPermission(request.UserPermissionId, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(DeleteUserRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetById(request.UserId);
        entity.DeleteUserRole(request.UserRoleId, _simaIdentity.UserId);
        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(CreateUserAggregateCommand request, CancellationToken cancellationToken)
    {
        var arg = _mapper.Map<CreateUserArg>(request);
        var entity = await User.Create(_service, arg);
        await _repository.Add(entity);

        if (request.FormUsers != null && request.FormUsers.Count != 0)
        {
            var formUserArg = _mapper.Map<List<CreateFormUserArg>>(request.FormUsers);
            foreach (var item in formUserArg)
            {
                item.CreatedBy = _simaIdentity.UserId;
                item.UserId = entity.Id.Value;
            }

            await entity.AddFormUser(formUserArg, entity.Id.Value, _service);
        }
        // todo: sanaz for test
        if (request.UserPermissions != null && request.UserPermissions.Count != 0)
        {
            var userPermissionsArgs = _mapper.Map<List<CreateUserPermissionArg>>(request.UserPermissions);
            foreach (var permission in userPermissionsArgs) permission.UserId = entity.Id.Value;
            foreach (var item in userPermissionsArgs) item.CreatedBy = _simaIdentity.UserId;
            await entity.AddUserPermission(userPermissionsArgs, entity.Id.Value);
        }

        if (request.UserRoles != null && request.UserRoles.Count != 0)
        {
            var userRolesArgs = _mapper.Map<List<CreateUserRoleArg>>(request.UserRoles);
            foreach (var role in userRolesArgs) role.UserId = entity.Id.Value;
            foreach (var item in userRolesArgs) item.CreatedBy = _simaIdentity.UserId;
            await entity.AddUserRole(userRolesArgs, entity.Id.Value);
        }

        if (request.UserGroups != null && request.UserGroups.Count != 0)
        {
            var userGroupsArgs = _mapper.Map<List<CreateUserGroupArg>>(request.UserGroups);
            foreach (var role in userGroupsArgs) role.UserId = entity.Id.Value;
            foreach (var item in userGroupsArgs) item.CreatedBy = _simaIdentity.UserId;
            await entity.AddUserGroup(userGroupsArgs, entity.Id.Value);
        }
        if (request.UserLocations != null && request.UserLocations.Count != 0)
        {
            var userLocationsArgs = _mapper.Map<List<CreateUserLocationAccessArg>>(request.UserLocations);
            foreach (var location in userLocationsArgs) location.UserId = entity.Id.Value;
            foreach (var item in userLocationsArgs) item.CreatedBy = _simaIdentity.UserId;
            await entity.AddUserLocation(userLocationsArgs, entity.Id.Value);
        }

        await _unitOfWork.SaveChangesAsync();
        return Result.Ok(entity.Id.Value);
    }
    public async Task<Result<long>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetUserForChangePassword(_simaIdentity.UserId, request.CurrentPassword);
        var arg = _mapper.Map<ChangePasswordArg>(request);
        await user.ChangePassword(arg);
        await _unitOfWork.SaveChangesAsync();

        return user.Id.Value;

    }
    public async Task<Result<long>> Handle(CheckUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetByUserName(request.UserName);
        var code = await user.GenerateCode(_service);

        #region Send SMS

        var userMobileNumber = await _queryrepository.GetUserMobileNumber(user.Id.Value);

        var sendsms = await _sMSService.SendSMS(new SendSMSRequest
        {
            DestinationAddress = userMobileNumber,
            MessageText = $"کد فعال سازی جهت ورود به سیستم {code}"
        });

        if (!sendsms.Succeeded)
            throw new SimaResultException(CodeMessges._400Code, Messages.FailedSendSMS);

        #endregion

        #region Expirtion Code
        string key = RedisKeys.SendSMS + user.Id.Value;
        TimeSpan expirtionTime = TimeSpan.FromMinutes(1);
        try
        {
            _redisService.Delete(key);
            await _redisService.InsertAsync(key, code, expirtionTime);
        }
        catch (Exception)
        {
            throw new SimaResultException(CodeMessges._100068Code, Messages.RedisConnectionError);
        }
        #endregion

        await _unitOfWork.SaveChangesAsync();
        return user.Id.Value;
    }
    public async Task<Result<long>> Handle(ConfirmCodeCommand request, CancellationToken cancellationToken)
    {
        string redisKey = RedisKeys.SendSMS + request.UserId;
        var code = await _redisService.GetAsync(redisKey);
        if (code is not null)
        {
            var user = await _repository.CheckForgetPasswordCode(request.UserId, request.Code);
            var password = user.GeneratePassword(_service);
            await _unitOfWork.SaveChangesAsync();

            #region Send SMS

            var userMobileNumber = await _queryrepository.GetUserMobileNumber(user.Id.Value);

            var sendsms = await _sMSService.SendSMS(new SendSMSRequest
            {
                DestinationAddress = userMobileNumber,
                MessageText = $"کلمه عبور شما با موفقیت تغییر یافت برای ورود از کلمه عبور جدید استفاده نمایید  {password}"
            });

            if (!sendsms.Succeeded)
                throw new SimaResultException(CodeMessges._400Code, Messages.FailedSendSMS);
            #endregion

            return user.Id.Value;
        }
        else
        {
            throw new SimaResultException(CodeMessges._400Code, Messages.CodeIsExpired);
        }

    }

    public async Task<Result<long>> Handle(ReSendOTPCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetByIdOnlyUser(request.UserId);
            string OTPCode = string.Empty;

            if (user.IsFirstLogin == "0" && user.IsSendOTP == "1")
            {
                OTPCode = await user.GenerateCode(_service);

                #region Send SMS

                var userMobileNumber = await _queryrepository.GetUserMobileNumber(user.Id.Value);

                var sendsms = await _sMSService.SendSMS(new SendSMSRequest
                {
                    DestinationAddress = userMobileNumber,
                    MessageText = $"کد فعال سازی جهت ورود به سیستم {OTPCode}"
                });

                if (!sendsms.Succeeded)
                    throw new SimaResultException(CodeMessges._400Code, Messages.FailedSendSMS);

                #endregion

                #region Expirtion Code
                string key = RedisKeys.SendSMS + user.Id.Value;
                TimeSpan expirtionTime = TimeSpan.FromMinutes(2);
                _redisService.Delete(key);
                await _redisService.InsertAsync(key, OTPCode, expirtionTime);
                #endregion
            }
            await _unitOfWork.SaveChangesAsync();
            return user.Id.Value;
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<Result<LoginUserQueryResult>> Handle(GetConfirmOTPCode request, CancellationToken cancellationToken)
    {
        string redisKey = RedisKeys.SendSMS + request.UserId;
        var code = await _redisService.GetAsync(redisKey);
        if (code is not null)
        {
            var user = await _repository.CheckForgetPasswordCode(request.UserId, request.Code);
            user.ConfirmOTpCode();
            await _unitOfWork.SaveChangesAsync();
            var permissions = await _queryrepository.GetPermissions(user.Id.Value);

            var result = UserMapper.MapToToken(permissions, _tokenService);

            //insert refreshToken in Redis
            await _redisService.InsertAsync(permissions.UserInfoLogin.Username, result.RefreshToken, TimeSpan.FromHours(_securitySettings.RefreshTokenLifeTime));

            return permissions;
        }
        else
        {
            throw new SimaResultException(CodeMessges._400Code, Messages.CodeIsExpired);
        }
    }

    public async Task<Result<LoginUserQueryResult>> Handle(SendOTPByUsernameCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _repository.GetByUserName(request.UserName);

            if (user.IsFirstLogin == "0" && user.IsSendOTP == "1")
            {
                var code = await user.GenerateCode(_service);

                #region Send SMS

                var userMobileNumber = await _queryrepository.GetUserMobileNumber(user.Id.Value);

                var sendsms = await _sMSService.SendSMS(new SendSMSRequest
                {
                    DestinationAddress = userMobileNumber,
                    MessageText = $"کد فعال سازی جهت ورود به سیستم {code}"
                });

                if (!sendsms.Succeeded)
                    throw new SimaResultException(CodeMessges._400Code, Messages.FailedSendSMS);

                #endregion

                #region Expirtion Code
                string key = RedisKeys.SendSMS + user.Id.Value;
                TimeSpan expirtionTime = TimeSpan.FromMinutes(3);
                _redisService.Delete(key);
                await _redisService.InsertAsync(key, code, expirtionTime);
                #endregion
            }
            await _unitOfWork.SaveChangesAsync();

            if (user.AccessFailedCount > 0)
                throw new SimaResultException(CodeMessges._400Code, Messages.InvalidUsernameOrPasswordError);

            if (user.IsLocked == "1")
                throw new SimaResultException(CodeMessges._400Code, Messages.UserIsLocked);

            var permissions = await _queryrepository.GetPermissions(user.Id.Value);
            permissions.UserInfoLogin.ConfirmCode = null;

            var result = UserMapper.MapToToken(permissions, _tokenService);

            //insert refreshToken in Redis
            await _redisService.InsertAsync(permissions.UserInfoLogin.Username, result.RefreshToken, TimeSpan.FromHours(_securitySettings.RefreshTokenLifeTime));

            return Result.Ok(result);
        }
        catch (Exception ex)
        {
            throw;
        }
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

            #endregion

            #region Permission

            CreateUserPermissionArg permissionArg = new CreateUserPermissionArg();
            var permissionArgList = new List<CreateUserPermissionArg>();
            permissionArg.UserId = Convert.ToInt64(userEntity.Id);
            permissionArg.PermissionId = 248;
            permissionArg.ActiveStatusId = (long)ActiveStatusEnum.Active;
            permissionArg.CreatedAt = DateTime.Now;
            permissionArgList.Add(permissionArg);
            await userEntity.AddUserPermission(permissionArgList, (long)permissionArg.UserId);
            #endregion
            await _unitOfWork.SaveChangesAsync();


            var tokenUser = await _repository.GetByUsernameAndPassword(userEntity.Username, userSSO.EmployeeCode);
            var permissions = await _queryrepository.GetPermissions(tokenUser.Id.Value);
            var result = UserMapper.MapToToken(permissions, _tokenService);
            return Result.Ok(result);
        }
        else
        {
            //TODO Change GetByUsernameAndPassword
            var tokenUser = await _repository.GetByUsernameAndPassword(user.Username, userSSO.EmployeeCode);
            var permissions = await _queryrepository.GetPermissions(tokenUser.Id.Value);
            var result = UserMapper.MapToToken(permissions, _tokenService);
            return Result.Ok(result);
        }
    }
}


