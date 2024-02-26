//using FluentValidation;
//using SIMA.Auth.Domain.Models.Users.Args;
//using SIMA.Auth.Domain.Models.Users.Interfaces;

//namespace SIMA.Auth.Domain.Models.Users.Validations;

//public class UserValidation : AbstractValidator<CreateUserArg>
//{
//    public UserValidation(IUserService userService)
//    {
//        RuleFor(u=>u.Username)
//            .NotEmpty()
//            .NotNull()
    
//          //  .Must(u=>userService.IsUsernameASCII(u))
//            .MustAsync(async (u, cancellationToken) => await userService.IsUsernameUnique(u));
//        RuleFor(u=>u.Password)
//            .NotEmpty()
//            .NotNull();
//    }
//}
