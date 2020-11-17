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
        Task<System.Guid> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam subcontractProfileTeam);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam> subcontractProfileTeamList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam subcontractProfileTeam);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam_PK> pkList);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>> SearchTeam(System.Guid companyId, System.Guid locationId, string teamcode
          , string teamNameTh, string teamNameEn, string storageLocation, string shipto);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeam>> GetByLocationId(System.Guid companyId, System.Guid locationId);

        Task<bool> DeleteTeamServiceSkill(string teamid);
        Task<bool> InsertTeamServiceSkill(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeamServiceSkill subcontractProfileTeamServiceSkill);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTeamServiceSkill>> GetServiceSkillByTeamId(System.Guid teamId);
    }
}
