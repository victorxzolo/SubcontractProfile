using Dapper;
using Repository;
using SubcontractProfile.WebApi.Services.Contracts;
using SubcontractProfile.WebApi.Services.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Services
{
    public partial class SubcontractProfileFileRepo: ISubcontractProfileFileRepo
    {
        private IDbContext _dbContext = null;

        public SubcontractProfileFileRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<bool> BulkInsert(IEnumerable<SubcontractProfileFile> subcontractProfileFileList)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteByPaymentId(string id)
        {
            var p = new DynamicParameters();
            p.Add("@payment_id", id);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileFile_deleteByPaymentId", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        public async Task<IEnumerable<SubcontractProfileFile>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileFile>
             ("uspSubcontractProfileFile_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        public async Task<IEnumerable<SubcontractProfileFile>> GetByPaymentId(string paymentid)
        {
            var p = new DynamicParameters();
            p.Add("@payment_id", paymentid);

            var entity = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileFile>
            ("uspSubcontractProfileFile_selectByPaymentId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        public async Task<bool> Insert(SubcontractProfileFile subcontractProfileFile)
        {
            var p = new DynamicParameters();

            p.Add("@upload_type", subcontractProfileFile.upload_type);
            p.Add("@payment_id", subcontractProfileFile.payment_id);
            p.Add("@file_Name", subcontractProfileFile.file_Name);
            p.Add("@create_by", subcontractProfileFile.CreateBy);
            p.Add("@company_id", subcontractProfileFile.company_id);
            p.Add("@fileid", subcontractProfileFile.file_id);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileFile_insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        public Task<bool> Update(SubcontractProfileFile subcontractProfileFile)
        {
            throw new NotImplementedException();
        }
    }
}
