using Dapper;
using Repository;
using SubcontractProfile.WebApi.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Threading.Tasks;

namespace SubcontractProfile.WebApi.Services.Services
{

    /// =================================================================
    /// Author: AIS Fibre - X10
    /// Description:	Class for the repo SubcontractProfileRegionRepo 
    /// =================================================================
    public partial class SubcontractProfileRegionRepo : ISubcontractProfileRegionRepo
    {

        protected IDbContext _dbContext = null;

        public SubcontractProfileRegionRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion>
            ("uspSubcontractProfileRegion_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion> GetByRegionId(int regionId)
        {
            var p = new DynamicParameters();
            p.Add("@region_id", regionId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion>
            ("uspSubcontractProfileRegion_selectByRegionId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion subcontractProfileRegion)
        {
            var p = new DynamicParameters();

            p.Add("@region_id", subcontractProfileRegion.RegionId);
            p.Add("@region_name", subcontractProfileRegion.RegionName);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileRegion_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion subcontractProfileRegion)
        {
            var p = new DynamicParameters();
            p.Add("@region_id", subcontractProfileRegion.RegionId);
            p.Add("@region_name", subcontractProfileRegion.RegionName);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileRegion_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(int regionId)
        {
            var p = new DynamicParameters();
            p.Add("@region_id", regionId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileRegion_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion> subcontractProfileRegionList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileRegionDataTable(subcontractProfileRegionList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileRegion_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileRegionDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion> SubcontractProfileRegionList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("region_id", typeof(SqlInt32));
            dt.Columns.Add("region_name", typeof(SqlString));

            if (SubcontractProfileRegionList != null)
                foreach (var curObj in SubcontractProfileRegionList)
                {
                    DataRow row = dt.NewRow();
                    row["region_id"] = new SqlInt32((int)curObj.RegionId);
                    row["region_name"] = new SqlString(curObj.RegionName);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileRegionPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion>
                ("uspSubcontractProfileRegion_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileRegionPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRegion_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("region_id", typeof(SqlInt32));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["region_id"] = new SqlInt32((int)curObj.RegionId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }
}
