//using FluentValidation;
//using SIMA.Auth.Domain.Models.Staffs.Args;
//using SIMA.Auth.Domain.Models.Staffs.Interfaces;

//namespace SIMA.Auth.Domain.Models.Staffs.Validations;

//public class StaffValidator : AbstractValidator<CreateStaffArg>
//{
//    public StaffValidator(IStaffService service)
//    {
//        RuleFor(s => s.Id)
//            .MustAsync(async (arg, x, cancellationToken) => await service.IsStaffSatisfied((long)arg.ProfileId, (long)arg.PositionId));
//    }
//}
