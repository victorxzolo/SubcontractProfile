using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileVerhicleTypeRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileVerhicleTypeRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType> GetByVerhicleTypeId(string verhicleTypeId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType subcontractProfileVerhicleType);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType> subcontractProfileVerhicleTypeList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType subcontractProfileVerhicleType);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType_PK> pkList);

    }
}
