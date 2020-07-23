using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{

    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileVerhicleBrandRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileVerhicleBrandRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand> GetByVerhicleBrandId(string verhicleBrandId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand subcontractProfileVerhicleBrand);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand> subcontractProfileVerhicleBrandList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand subcontractProfileVerhicleBrand);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand_PK> pkList);

    }
}
