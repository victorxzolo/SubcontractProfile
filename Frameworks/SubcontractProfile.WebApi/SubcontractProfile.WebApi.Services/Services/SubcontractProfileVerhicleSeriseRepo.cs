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
    /// Description:	Class for the repo SubcontractProfileVerhicleSeriseRepo 
    /// =================================================================
    public partial class SubcontractProfileVerhicleSeriseRepo : ISubcontractProfileVerhicleSeriseRepo
    {

        protected IDbContext _dbContext;

        public SubcontractProfileVerhicleSeriseRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise>
            ("uspSubcontractProfileVerhicleSerise_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise> GetByVerhicleSeriseId(string verhicleSeriseId)
        {
            var p = new DynamicParameters();
            p.Add("@verhicle_serise_id", verhicleSeriseId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise>
            ("uspSubcontractProfileVerhicleSerise_selectByVerhicleSeriseId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise subcontractProfileVerhicleSerise)
        {
            var p = new DynamicParameters();

            p.Add("@verhicle_serise_id", subcontractProfileVerhicleSerise.VerhicleSeriseId);
            p.Add("@verhicle_serise_name", subcontractProfileVerhicleSerise.VerhicleSeriseName);
            p.Add("@verhicle_brand_id", subcontractProfileVerhicleSerise.VerhicleBrandId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleSerise_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise subcontractProfileVerhicleSerise)
        {
            var p = new DynamicParameters();
            p.Add("@verhicle_serise_id", subcontractProfileVerhicleSerise.VerhicleSeriseId);
            p.Add("@verhicle_serise_name", subcontractProfileVerhicleSerise.VerhicleSeriseName);
            p.Add("@verhicle_brand_id", subcontractProfileVerhicleSerise.VerhicleBrandId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleSerise_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string verhicleSeriseId)
        {
            var p = new DynamicParameters();
            p.Add("@verhicle_serise_id", verhicleSeriseId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleSerise_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise> subcontractProfileVerhicleSeriseList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileVerhicleSeriseDataTable(subcontractProfileVerhicleSeriseList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleSerise_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileVerhicleSeriseDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise> SubcontractProfileVerhicleSeriseList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("verhicle_serise_id", typeof(SqlString));
            dt.Columns.Add("verhicle_serise_name", typeof(SqlString));
            dt.Columns.Add("verhicle_brand_id", typeof(SqlString));

            if (SubcontractProfileVerhicleSeriseList != null)
                foreach (var curObj in SubcontractProfileVerhicleSeriseList)
                {
                    DataRow row = dt.NewRow();
                    row["verhicle_serise_id"] = new SqlString(curObj.VerhicleSeriseId);
                    row["verhicle_serise_name"] = new SqlString(curObj.VerhicleSeriseName);
                    row["verhicle_brand_id"] = new SqlString(curObj.VerhicleBrandId);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileVerhicleSerisePKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise>
                ("uspSubcontractProfileVerhicleSerise_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileVerhicleSerisePKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("verhicle_serise_id", typeof(SqlString));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["verhicle_serise_id"] = new SqlString(curObj.VerhicleSeriseId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise>> GetByVerhicleBrandId(string verhicleBrandId)
        {
            var p = new DynamicParameters();
            p.Add("@verhicle_brand_id", verhicleBrandId);

            var entity = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleSerise>
            ("uspSubcontractProfileVerhicleSerise_selectByVerhicleBrandId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

    }
}
