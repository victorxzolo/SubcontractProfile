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
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining> GetByTrainingId(System.Guid trainingId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining subcontractProfileTraining);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining> subcontractProfileTrainingList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining subcontractProfileTraining);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining_PK> pkList);
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining> SearchTraining(string company_id,
            string location_code, string team_id, string staff_name_th, string position_id, string status, string date_from, string date_to);

    }
}
