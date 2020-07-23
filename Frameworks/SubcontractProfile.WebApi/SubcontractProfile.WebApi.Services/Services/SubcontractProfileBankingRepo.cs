using Dapper;
using SubcontractProfile.WebApi.Services.Contracts;
using SubcontractProfile.WebApi.Services.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Services
{
    /// =================================================================
    /// Author: AIS Fibre
    /// Description:	Class for the repo SubcontractProfileBankingRepo 
    /// =================================================================
    public partial class SubcontractProfileBankingRepo : ISubcontractProfileBankingRepo
    {

        protected Repository.DbContext _dbContext = null;

        public SubcontractProfileBankingRepo(Repository.DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking>
            ("uspSubcontractProfileBanking_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking> GetByBankId(System.Guid bankId)
        {
            var p = new DynamicParameters();
            p.Add("@bank_id", bankId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking>
            ("uspSubcontractProfileBanking_selectByBankId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking subcontractProfileBanking)
        {
            var p = new DynamicParameters();

            p.Add("@bank_id", subcontractProfileBanking.BankId);
            p.Add("@bank_code", subcontractProfileBanking.BankCode);
            p.Add("@bank_name", subcontractProfileBanking.BankName);
            p.Add("@bank_branch", subcontractProfileBanking.BankBranch);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileBanking_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking subcontractProfileBanking)
        {
            var p = new DynamicParameters();
            p.Add("@bank_id", subcontractProfileBanking.BankId);
            p.Add("@bank_code", subcontractProfileBanking.BankCode);
            p.Add("@bank_name", subcontractProfileBanking.BankName);
            p.Add("@bank_branch", subcontractProfileBanking.BankBranch);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileBanking_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(int bankId)
        {
            var p = new DynamicParameters();
            p.Add("@bank_id", bankId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileBanking_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking> subcontractProfileBankingList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileBankingDataTable(subcontractProfileBankingList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileBanking_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileBankingDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking> SubcontractProfileBankingList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("bank_id", typeof(SqlGuid));
            dt.Columns.Add("bank_code", typeof(SqlString));
            dt.Columns.Add("bank_name", typeof(SqlString));
            dt.Columns.Add("bank_branch", typeof(SqlString));

            if (SubcontractProfileBankingList != null)
                foreach (var curObj in SubcontractProfileBankingList)
                {
                    DataRow row = dt.NewRow();
                    row["bank_id"] = new SqlGuid(curObj.BankId);
                    row["bank_code"] = new SqlString(curObj.BankCode);
                    row["bank_name"] = new SqlString(curObj.BankName);
                    row["bank_branch"] = new SqlString(curObj.BankBranch);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileBankingPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking>
                ("uspSubcontractProfileBanking_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileBankingPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileBanking_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("bank_id", typeof(SqlGuid));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["bank_id"] = new SqlGuid(curObj.BankId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

      
    }
}
