using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileCompanyTypeRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileCompanyTypeRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType> GetByCompanyTypeId(string companyTypeId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType subcontractProfileCompanyType);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType> subcontractProfileCompanyTypeList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType subcontractProfileCompanyType);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType_PK> pkList);


    }
}
