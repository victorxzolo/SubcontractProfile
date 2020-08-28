using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfilePaymentRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfilePaymentRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment> GetByPaymentId(string paymentId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment subcontractProfilePayment);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment> subcontractProfilePaymentList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment subcontractProfilePayment);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment_PK> pkList);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment>> searchPayment(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment search);
    }
}
