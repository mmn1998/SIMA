//using FluentValidation;
//using SIMA.Auth.Domain.Models.Profiles.Args;
//using SIMA.Auth.Domain.Models.Profiles.Interfaces;

//namespace SIMA.Auth.Domain.Models.Profiles.Validations;

//public class AddressBookValidator : AbstractValidator<CreateAddressBookArg>
//{
//    public AddressBookValidator(IProfileService service)
//    {
//        RuleFor(x => x.PostalCode)
//            .MinimumLength(10)
//            .MaximumLength(10);

//            //.Must(x => service.IsPostalcodeValid(x));
//    }
//}
