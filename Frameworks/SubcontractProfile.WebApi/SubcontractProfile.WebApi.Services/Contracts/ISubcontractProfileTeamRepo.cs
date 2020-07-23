using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileTeamRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileTeamRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam> GetByTeamId(System.Guid teamId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam subcontractProfileTeam);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam> subcontractProfileTeamList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam subcontractProfileTeam);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam_PK> pkList);

    }
}
