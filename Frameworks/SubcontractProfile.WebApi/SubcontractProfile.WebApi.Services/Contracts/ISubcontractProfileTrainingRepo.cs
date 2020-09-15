using SubcontractProfile.WebApi.Services.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileTrainingRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileTrainingRepo
    {
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingRequest> GetByTrainingId(System.Guid trainingId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingRequest subcontractProfileTraining);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining> subcontractProfileTrainingList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingRequest subcontractProfileTraining);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining_PK> pkList);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>> SearchTraining(Guid company_id,string status, string date_from, string date_to);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>> SearchTrainingForApprove(string company_name_th,
         string tax_id, string status, string date_from, string date_to, string bookingdate_from, string bookingdate_to);
        Task<bool> UpdateByVerified(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingRequest subcontractProfileTraining);

        Task<bool> UpdateTestResult(SubcontractProfileTrainingRequest subcontractProfileTraining);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>> SearchTrainingForTest(string company_name_th,
       string tax_id, string training_date_fr, string training_date_to, string test_date_fr, string test_date_to,string status);

    }
}
