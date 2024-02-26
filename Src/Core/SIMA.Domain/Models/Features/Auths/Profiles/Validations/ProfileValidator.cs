//using FluentValidation;
//using SIMA.Auth.Domain.Models.Profiles.Args;
//using SIMA.Auth.Domain.Models.Profiles.Interfaces;
//using SIMA.Framework.Common.Helper;

//namespace SIMA.Auth.Domain.Models.Profiles.Validations;

//public class ProfileValidator : AbstractValidator<CreateProfileArg>
//{
//    public ProfileValidator()
//    {
//        RuleFor(p => p.NationalId)
//            .NotNull()
//            .NotEmpty()
//            .MaximumLength(11)
//            .MinimumLength(10)
//            .Must(x => RegexHelper.IsNationalID(x));
//    }

//}
