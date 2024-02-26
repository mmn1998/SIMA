//using FluentValidation;
//using SIMA.Auth.Domain.Models.ConfigurationAttributes.Args;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace SIMA.Auth.Domain.Models.ConfigurationAttributes.Validations
//{
//    public class ConfigurationAttributeValidator : AbstractValidator<CreateConfigurationAttributeArg>
//    {
//        public ConfigurationAttributeValidator(IConfigurationAttributeService service) {
//            RuleFor(x => x.EnglishKey)
//                .MustAsync(async (x, CancellationToken) => await service.CheckEnglishKey(x));
//        }
//    }
//}
