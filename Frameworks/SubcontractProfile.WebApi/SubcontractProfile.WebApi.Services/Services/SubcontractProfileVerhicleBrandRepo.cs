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
    /// Description:	Class for the repo SubcontractProfileVerhicleBrandRepo 
    /// =================================================================
    public partial class SubcontractProfileVerhicleBrandRepo : ISubcontractProfileVerhicleBrandRepo
    {

        protected Repository.DbContext _dbContext = null;

        public SubcontractProfileVerhicleBrandRepo(Repository.DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand>
            ("uspSubcontractProfileVerhicleBrand_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand> GetByVerhicleBrandId(string verhicleBrandId)
        {
            var p = new DynamicParameters();
            p.Add("@verhicle_brand_id", verhicleBrandId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand>
            ("uspSubcontractProfileVerhicleBrand_selectByVerhicleBrandId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand subcontractProfileVerhicleBrand)
        {
            var p = new DynamicParameters();

            p.Add("@verhicle_brand_id", subcontractProfileVerhicleBrand.VerhicleBrandId);
            p.Add("@verhicle_brand_name", subcontractProfileVerhicleBrand.VerhicleBrandName);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleBrand_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand subcontractProfileVerhicleBrand)
        {
            var p = new DynamicParameters();
            p.Add("@verhicle_brand_id", subcontractProfileVerhicleBrand.VerhicleBrandId);
            p.Add("@verhicle_brand_name", subcontractProfileVerhicleBrand.VerhicleBrandName);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleBrand_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string verhicleBrandId)
        {
            var p = new DynamicParameters();
            p.Add("@verhicle_brand_id", verhicleBrandId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleBrand_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand> subcontractProfileVerhicleBrandList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileVerhicleBrandDataTable(subcontractProfileVerhicleBrandList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleBrand_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileVerhicleBrandDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand> SubcontractProfileVerhicleBrandList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("verhicle_brand_id", typeof(SqlString));
            dt.Columns.Add("verhicle_brand_name", typeof(SqlString));

            if (SubcontractProfileVerhicleBrandList != null)
                foreach (var curObj in SubcontractProfileVerhicleBrandList)
                {
                    DataRow row = dt.NewRow();
                    row["verhicle_brand_id"] = new SqlString(curObj.VerhicleBrandId);
                    row["verhicle_brand_name"] = new SqlString(curObj.VerhicleBrandName);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileVerhicleBrandPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand>
                ("uspSubcontractProfileVerhicleBrand_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileVerhicleBrandPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleBrand_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("verhicle_brand_id", typeof(SqlString));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["verhicle_brand_id"] = new SqlString(curObj.VerhicleBrandId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }

}
