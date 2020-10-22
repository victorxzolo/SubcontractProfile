using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Contracts
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Interface for the repo ISubcontractProfileUserRepo 
    /// ================================================================= 
    public partial interface ISubcontractProfileUserRepo
    {

        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser>> GetAll();
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser> GetByUserId(System.Guid userId);
        Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser subcontractProfileUser);
        Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser> subcontractProfileUserList);
        Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser subcontractProfileUser);
        Task<bool> Delete(string id);
        Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser_PK> pkList);
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser> LoginUser(string username, string password);
        Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser> LoginUserSSO(string username);
    }
}
