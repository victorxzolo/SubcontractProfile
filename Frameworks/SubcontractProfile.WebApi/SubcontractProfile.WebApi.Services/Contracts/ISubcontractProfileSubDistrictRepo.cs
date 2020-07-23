using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{

    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileSubDistrictRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileSubDistrictRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict> GetBySubDistrictId(int subDistrictId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict subcontractProfileSubDistrict);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict> subcontractProfileSubDistrictList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict subcontractProfileSubDistrict);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict_PK> pkList);

    }
}
