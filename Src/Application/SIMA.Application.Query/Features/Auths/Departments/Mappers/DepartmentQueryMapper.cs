using AutoMapper;
using SIMA.Application.Query.Contract.Features.Auths.Departments;
using SIMA.Domain.Models.Features.Auths.Departments.Entities;

namespace SIMA.Application.Query.Features.Auths.Departments.Mappers;

internal class DepartmentQueryMapper : Profile
{
    public DepartmentQueryMapper()
    {
        CreateMap<Department, GetDepartmentQueryResult>();
    }
}
