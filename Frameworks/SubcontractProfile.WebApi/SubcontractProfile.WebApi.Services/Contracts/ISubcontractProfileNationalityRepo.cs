using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileNationalityRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileNationalityRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality> GetByNationalityId(string nationalityId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality subcontractProfileNationality);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality> subcontractProfileNationalityList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality subcontractProfileNationality);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality_PK> pkList);

    }
}
