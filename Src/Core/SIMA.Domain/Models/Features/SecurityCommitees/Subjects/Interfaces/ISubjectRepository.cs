using SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Entities;
using SIMA.Framework.Core.Repository;

namespace SIMA.Domain.Models.Features.SecurityCommitees.Subjects.Interfaces;

public interface ISubjectRepository : IRepository<Subject>
{
    Task<Subject> GetById(long Id);
}