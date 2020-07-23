using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileProvinceRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileProvinceRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince> GetByProvinceId(int provinceId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince subcontractProfileProvince);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince> subcontractProfileProvinceList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince subcontractProfileProvince);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince_PK> pkList);

    }
}
