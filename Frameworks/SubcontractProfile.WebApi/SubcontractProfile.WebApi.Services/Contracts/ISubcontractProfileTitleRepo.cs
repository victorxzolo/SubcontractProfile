using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileTitleRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileTitleRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle> GetByTitleId(string titleId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle subcontractProfileTitle);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle> subcontractProfileTitleList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle subcontractProfileTitle);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle_PK> pkList);

    }
}
