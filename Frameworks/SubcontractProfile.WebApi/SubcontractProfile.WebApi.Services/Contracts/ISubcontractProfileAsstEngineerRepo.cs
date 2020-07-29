using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileAsstEngineerRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileAsstEngineerRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer> GetByAsstEngineerId(string asstEngineerId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer subcontractProfileAsstEngineer);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer> subcontractProfileAsstEngineerList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer subcontractProfileAsstEngineer);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer_PK> pkList);

    }

}
