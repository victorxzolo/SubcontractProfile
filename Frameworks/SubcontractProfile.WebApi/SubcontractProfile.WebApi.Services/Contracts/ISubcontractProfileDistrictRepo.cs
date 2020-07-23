using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileDistrictRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileDistrictRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict> GetByDistrictId(int districtId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict subcontractProfileDistrict);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict> subcontractProfileDistrictList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict subcontractProfileDistrict);
        Task<bool> Delete(int id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict_PK> pkList);

    }
}
