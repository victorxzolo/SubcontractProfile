using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{

    /// =================================================================
    /// Author: AIS-X10
    /// Description:	Interface for the repo ISubcontractProfileTrainingEngineerRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileTrainingEngineerRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer> GetByTrainingEngineerId(System.Guid trainingEngineerId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer subcontractProfileTrainingEngineer);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer> subcontractProfileTrainingEngineerList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer subcontractProfileTrainingEngineer);
        Task<bool> Delete(System.Guid id);
        Task<bool> DeleteByTriningId(System.Guid trainingId);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer_PK> pkList);

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer>> GetTrainingEngineerByTrainingId(System.Guid training_Id);

        Task<bool> UpdateByTestResult(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer subcontractProfileTrainingEngineer);

        
    }
}
