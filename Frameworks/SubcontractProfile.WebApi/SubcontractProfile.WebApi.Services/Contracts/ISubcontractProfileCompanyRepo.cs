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
        Task<bool> UpdateByActivate(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany subcontractProfileCompany);

        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany_PK> pkList);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> SearchCompany(Guid companyId
            , string company_th, string company_en, string company_alias, string tax_id,string subcontract_profile_type
           );

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> SearchCompanyVerify(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany search);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany>> SearchActivateProfile(string subcontract_profile_type
           , string company_name_th, string tax_id, string activate_date_fr, string activate_date_to, string activate_status
          );
        Task<bool> UpdateVerify(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompany subcontractProfileCompany);
    }
}
