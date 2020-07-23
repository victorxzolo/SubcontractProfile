using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileRaceRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileRaceRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace> GetByRaceId(string raceId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace subcontractProfileRace);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace> subcontractProfileRaceList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace subcontractProfileRace);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace_PK> pkList);

    }
}
