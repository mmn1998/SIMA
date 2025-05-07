using SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.SecurityCommitees.ResponsibleAnswerTypes.Interfaces;

public interface IResponsibleAnswerTypeRepository : IRepository<ResponsibleAnswerType>
{
    Task<ResponsibleAnswerType> GetById(long Id);
}
