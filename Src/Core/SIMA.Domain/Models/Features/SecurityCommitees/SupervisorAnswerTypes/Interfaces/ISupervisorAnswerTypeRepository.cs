using SIMA.Domain.Models.Features.SecurityCommitees.SupervisorAnswerTypes.Entities;
using SIMA.Framework.Core.Repository;

public interface ISupervisorAnswerTypeRepository : IRepository<SupervisorAnswerType>
{
    Task<SupervisorAnswerType> GetById(long Id);
}