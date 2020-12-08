using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    public partial interface ISubcontractProfileFileRepo
    {
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileFile>> GetAll();
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileFile>> GetByPaymentId(string paymentid);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileFile subcontractProfileFile);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileFile> subcontractProfileFileList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileFile subcontractProfileFile);
        Task<bool> DeleteByPaymentId(string id);
    }
}
