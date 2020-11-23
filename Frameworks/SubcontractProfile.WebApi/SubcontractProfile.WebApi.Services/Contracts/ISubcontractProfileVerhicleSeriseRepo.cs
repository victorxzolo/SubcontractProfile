using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileVerhicleSeriseRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileVerhicleSeriseRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise> GetByVerhicleSeriseId(string verhicleSeriseId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise subcontractProfileVerhicleSerise);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise> subcontractProfileVerhicleSeriseList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise subcontractProfileVerhicleSerise);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise_PK> pkList);


        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise>> GetByVerhicleBrandId(string verhicleBrandId);
    }
}
