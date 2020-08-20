using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileEngineerRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileEngineerRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer> GetByEngineerId(System.Guid engineerId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer subcontractProfileEngineer);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer> subcontractProfileEngineerList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer subcontractProfileEngineer);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer_PK> pkList);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> SearchEngineer(Guid companyId, Guid locationId,
            Guid teamId,string staffName, string citizenId, string position);

    }
}
