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
    public partial interface ISubcontractProfileCompanyBLL
    {
        Task<IEnumerable<SubcontractProfileCompany>> GetByQueryCompanyId(SubcontractProfileCompany subcontractProfileCompany);
   
    }

    public partial class SubcontractProfileCompanyBLL : ISubcontractProfileCompanyBLL, ISubcontractProfileCompanyRepo
    {
        private readonly ILogger<SubcontractProfileCompanyBLL> _log;
         private readonly SubContractProfile.Infrastructure.DbContext _dbContext;
        private readonly IConfiguration _config;
        private readonly ISubcontractProfileCompanyRepo _companyRepo;

        //public SubcontractProfileCompanyBLL( ISubcontractProfileCompanyRepo profileComp,
        // IDbContext context)
        //{
        //   // _log = logger;
        //    _profileComp = profileComp;
        //    _dbContext = context;
        //}


        public SubcontractProfileCompanyBLL(IConfiguration config, ILogger<SubcontractProfileCompanyBLL> logger
            , ISubcontractProfileCompanyRepo companyRepo)
        {
             _log = logger;
            _dbContext = new DbContext(config);
            _companyRepo = companyRepo;
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

        Task<bool> ISubcontractProfileCompanyRepo.BulkInsert(IEnumerable<SubcontractProfileCompany> subcontractProfileCompanyList)
        {
            return _companyRepo.BulkInsert(subcontractProfileCompanyList);
          
        }

        Task<bool> ISubcontractProfileCompanyRepo.Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<SubcontractProfileCompany>> ISubcontractProfileCompanyRepo.GetAll()
        {
            return _companyRepo.GetAll();
        }

        Task<SubcontractProfileCompany> ISubcontractProfileCompanyRepo.GetByCompanyId(Guid companyId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<SubcontractProfileCompany>> ISubcontractProfileCompanyRepo.GetByPKList(IEnumerable<SubcontractProfileCompany_PK> pkList)
        {
            throw new NotImplementedException();
        }

        Task<bool> ISubcontractProfileCompanyRepo.Insert(SubcontractProfileCompany subcontractProfileCompany)
        {
            throw new NotImplementedException();
        }

        Task<bool> ISubcontractProfileCompanyRepo.Update(SubcontractProfileCompany subcontractProfileCompany)
        {
            throw new NotImplementedException();
        }
    }
}
