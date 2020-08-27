using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    public partial interface ISubcontractProfileRequestStatusRepo
    {
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRequestStatus>> GetAll();
    }
}
