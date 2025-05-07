using AutoMapper;
using SIMA.Application.Contract.Features.BranchManagement.FinancialSuppliers;
using SIMA.Domain.Models.Features.BranchManagement.FinancialSuppliers.Args;
using SIMA.Framework.Common.Helper;
using SIMA.Framework.Common.Security;
using System.Text;

namespace SIMA.Application.Feaatures.BranchManagement.FinancialSuppliers.Mapper
{
    public class FinancialSupplierMapper : Profile
    {
        public FinancialSupplierMapper()
        {
            CreateMap<CreateFinancialSupplierCommand, CreateFinancialSupplierArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.CreatedAt, act => act.MapFrom(source => DateTime.Now));

            CreateMap<ModifyFinancialSupplierCommand, ModifyFinancialSupplierArg>()
                .ForMember(dest => dest.ActiveStatusId, act => act.MapFrom(source => (long)ActiveStatusEnum.Active))
                .ForMember(dest => dest.ModifiedAt, act => act.MapFrom(source => Encoding.UTF8.GetBytes(DateTime.Now.ToString())));
        }
    }
}
