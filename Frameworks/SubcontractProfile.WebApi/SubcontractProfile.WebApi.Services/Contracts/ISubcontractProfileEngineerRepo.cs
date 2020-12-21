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
        Task<bool> MigrationInsert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer subcontractProfileLocation);

        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer> subcontractProfileEngineerList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer subcontractProfileEngineer);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer_PK> pkList);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> SearchEngineer(Guid companyId, Guid locationId,
            Guid teamId,string staffName, string citizenId, string position);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> selectEngineerAll(string citizen_id, string staff_name, string contract_phone, string date_from, string date_to);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> selectEngineer(string citizen_id, string staff_name, string contact_phone, string date_from, string date_to);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> GetEngineerByTeam(System.Guid companyId, System.Guid locationId, System.Guid teamId);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> GetEngineerByCompany(System.Guid companyId);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineerBlacklist>> CheckBlacklist(string id_card);
    }
}
