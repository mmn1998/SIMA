using SIMA.Domain.Models.Features.SecurityCommitees.Labels.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;

namespace SIMA.Application.Feaatures.SecurityCommitees.Labels.Mappers
{
    public class LabelMapper 
    {
        private static ISimaIdentity _simaIdentity;

        public LabelMapper(ISimaIdentity simaIdentity)
        {
            _simaIdentity = simaIdentity;
        }
        public static List<CreateLabelArg> Map(string value)
        {
            var labels = value.Split(',');
            var result = new List<CreateLabelArg>();
            foreach (var label in labels)
            {
                var item = new CreateLabelArg
                {
                    Id = IdHelper.GenerateUniqueId(),
                    Code = label,
                    ActiveStatusId = (long)ActiveStatusEnum.Active,
                    CreatedAt = DateTime.Now,
                    CreatedBy = _simaIdentity.UserId,

                };
                result.Add(item);
            }
            return result;
        }
    }
}
