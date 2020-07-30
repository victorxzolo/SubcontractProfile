using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{

    /// =================================================================
    /// Author: AIS Fibre - X10
    /// Description:	Interface for the repo ISubcontractProfileRegionRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileRegionRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion> GetByRegionId(int regionId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion subcontractProfileRegion);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion> subcontractProfileRegionList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion subcontractProfileRegion);
        Task<bool> Delete(int id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion_PK> pkList);

    }
}
