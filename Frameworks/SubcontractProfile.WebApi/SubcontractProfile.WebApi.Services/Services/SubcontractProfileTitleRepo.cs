using Dapper;
using Repository;
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
    /// Description:	Class for the repo SubcontractProfileTitleRepo 
    /// =================================================================
    public partial class SubcontractProfileTitleRepo : ISubcontractProfileTitleRepo
    {

        protected IDbContext _dbContext;

        public SubcontractProfileTitleRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle>
            ("uspSubcontractProfileTitle_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle> GetByTitleId(string titleId)
        {
            var p = new DynamicParameters();
            p.Add("@title_id", titleId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle>
            ("uspSubcontractProfileTitle_selectByTitleId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle subcontractProfileTitle)
        {
            var p = new DynamicParameters();

            p.Add("@title_id", subcontractProfileTitle.TitleId);
            p.Add("@title_name_th", subcontractProfileTitle.TitleNameTh);
            p.Add("@title_name_en", subcontractProfileTitle.TitleNameEn);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTitle_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle subcontractProfileTitle)
        {
            var p = new DynamicParameters();
            p.Add("@title_id", subcontractProfileTitle.TitleId);
            p.Add("@title_name_th", subcontractProfileTitle.TitleNameTh);
            p.Add("@title_name_en", subcontractProfileTitle.TitleNameEn);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTitle_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string titleId)
        {
            var p = new DynamicParameters();
            p.Add("@title_id", titleId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTitle_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle> subcontractProfileTitleList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileTitleDataTable(subcontractProfileTitleList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTitle_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileTitleDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle> SubcontractProfileTitleList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("title_id", typeof(SqlString));
            dt.Columns.Add("title_name_th", typeof(SqlString));
            dt.Columns.Add("title_name_en", typeof(SqlString));

            if (SubcontractProfileTitleList != null)
                foreach (var curObj in SubcontractProfileTitleList)
                {
                    DataRow row = dt.NewRow();
                    row["title_id"] = new SqlString(curObj.TitleId);
                    row["title_name_th"] = new SqlString(curObj.TitleNameTh);
                    row["title_name_en"] = new SqlString(curObj.TitleNameEn);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileTitlePKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle>
                ("uspSubcontractProfileTitle_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileTitlePKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTitle_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("title_id", typeof(SqlString));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["title_id"] = new SqlString(curObj.TitleId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }
}
