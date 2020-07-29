using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileAddressRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileAddressRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress> GetByAddressId(string addressId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress subcontractProfileAddress);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress> subcontractProfileAddressList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress subcontractProfileAddress);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddress_PK> pkList);

    }
}
