using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileBankingRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileBankingRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking> GetByBankId(System.Guid bankId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking subcontractProfileBanking);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking> subcontractProfileBankingList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking subcontractProfileBanking);
        Task<bool> Delete(int id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking_PK> pkList);

    }
}
