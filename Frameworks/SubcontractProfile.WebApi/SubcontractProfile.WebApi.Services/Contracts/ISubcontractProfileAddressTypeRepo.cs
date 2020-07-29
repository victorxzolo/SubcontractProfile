using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre -X10
    /// Description:	Interface for the repo ISubcontractProfileAddressTypeRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileAddressTypeRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType> GetByAddressTypeId(string addressTypeId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType subcontractProfileAddressType);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType> subcontractProfileAddressTypeList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAddressType subcontractProfileAddressType);
        Task<bool> Delete(string id);

    }
}
