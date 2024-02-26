//using FluentValidation;
//using SIMA.Auth.Domain.Models.Roles.Args;
//using SIMA.Auth.Domain.Models.Roles.Interfaces;

//namespace SIMA.Auth.Domain.Models.Roles.Validations;

//public class RoleValidator : AbstractValidator<CreateRoleArg>
//{
//    public RoleValidator(IRoleService service)
//    {
//        RuleFor(x => x.Code)
//          .MustAsync(async (arg, x, cancellationToken) => await service.IsRoleSatisfied(arg.Code, arg.EnglishKey));
//    }
//}
