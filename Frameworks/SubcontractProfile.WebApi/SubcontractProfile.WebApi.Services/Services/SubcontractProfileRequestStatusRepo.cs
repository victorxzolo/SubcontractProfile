using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Repository;
using SubcontractProfile.WebApi.Services.Contracts;
using SubcontractProfile.WebApi.Services.Model;

namespace SubcontractProfile.WebApi.Services.Services
{
    public partial class SubcontractProfileRequestStatusRepo : ISubcontractProfileRequestStatusRepo
    {
        protected IDbContext _dbContext;

        public SubcontractProfileRequestStatusRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IEnumerable<SubcontractProfileRequestStatus>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRequestStatus>
              ("uspSubcontractRequestStatus_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }
    }
}
