using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileCompanyRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileCompanyRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany> GetByCompanyId(System.Guid companyId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany subcontractProfileCompany);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany> subcontractProfileCompanyList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany subcontractProfileCompany);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany_PK> pkList);
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany> SearchCompany(string subcontract_profile_type
            ,string location_code, string vendor_code, string company_th
            , string company_en, string company_alias, string company_code
            , string distibution_channel, string channel_sale_group);

    }
}
