using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileReligionRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileReligionRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion> GetByReligionId(string religionId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion subcontractProfileReligion);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion> subcontractProfileReligionList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion subcontractProfileReligion);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion_PK> pkList);

    }
}
