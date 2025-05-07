//using FluentValidation;
//using SIMA.Auth.Domain.Models.Users.Args;
//using SIMA.Auth.Domain.Models.Users.Interfaces;
//using System.Data;

//namespace SIMA.Auth.Domain.Models.Users.Validations;

//public class UserConfigValidation : AbstractValidator<CreateUserConfigArg>
//{
//    public UserConfigValidation(IUserService service)
//    {
//        RuleFor(uc => uc.Id)
//            .MustAsync(async (arg, id, cancellationToken) => await service.IsUsrConfigSatisfied(arg.ConfigurationId, arg.UserId));
//    }
//}
