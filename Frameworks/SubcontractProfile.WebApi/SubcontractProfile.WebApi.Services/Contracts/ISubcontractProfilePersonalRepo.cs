using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfilePersonalRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfilePersonalRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal> GetByPersonalId(System.Guid personalId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal subcontractProfilePersonal);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal> subcontractProfilePersonalList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal subcontractProfilePersonal);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal>> selectPersonalAll(string citizen_id, string full_name, string contact_phone, string date_from, string date_to);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal>> selectPersonal(string citizen_id, string full_name, string contact_phone, string date_from, string date_to);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal_PK> pkList);

    }

}
