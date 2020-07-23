using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileWarrantyRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileWarrantyRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty> GetByWarrantyId(string warrantyId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty subcontractProfileWarranty);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty> subcontractProfileWarrantyList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty subcontractProfileWarranty);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty_PK> pkList);

    }
}
