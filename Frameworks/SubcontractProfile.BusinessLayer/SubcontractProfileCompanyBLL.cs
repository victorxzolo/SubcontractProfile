using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SubContractProfile.Infrastructure;
using SubcontractProfile.Core.Entities;

namespace SubcontractProfile.BusinessLayer
{
    public class SubcontractProfileCompanyBLL 
    {
        private readonly ILogger<SubcontractProfileCompanyRepo> _log;
        private readonly ISubcontractProfileCompanyRepo _profileComp;
        private readonly IDbContext _dbContext;
        public SubcontractProfileCompanyBLL(ILogger<SubcontractProfileCompanyRepo> logger, ISubcontractProfileCompanyRepo profileComp,
         IDbContext context)
        {
            _log = logger;
            _profileComp = profileComp;
            _dbContext = context;
        }


        public async Task<IEnumerable<SubcontractProfileCompany>> GetByQueryCompanyId(SubcontractProfileCompany subcontractProfileCompany)
        {
            _log.LogInformation("Start Log message in the GetByQueryCompanyId() method {0}", DateTime.Now);
            IEnumerable<SubcontractProfileCompany> entity = null;

            try
            {
                var p = new DynamicParameters();
                p.Add("@company_id", subcontractProfileCompany.CompanyId);

                entity = await _dbContext.Connection.QueryAsync<SubcontractProfileCompany>
             ("uspSubcontractProfileCompany_selectByCompanyId", p, commandType: CommandType.StoredProcedure);

                _log.LogInformation("Successfully get database {0}", subcontractProfileCompany);
            }
            catch (Exception ex)
            {
                _log.LogError("Failed get database.", ex.Message.ToString());

            }
            _log.LogInformation("End Log message in the GetByQueryCompanyId() method {0}", DateTime.Now);

            return entity;
        }


    }
}
