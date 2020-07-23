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
    /// Description:	Class for the repo SubcontractProfileDistrictRepo 
    /// =================================================================
    public partial class SubcontractProfileDistrictRepo : ISubcontractProfileDistrictRepo
    {

        protected Repository.DbContext _dbContext = null;

        public SubcontractProfileDistrictRepo(Repository.DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict>
            ("uspSubcontractProfileDistrict_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict> GetByDistrictId(int districtId)
        {
            var p = new DynamicParameters();
            p.Add("@district_id", districtId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict>
            ("uspSubcontractProfileDistrict_selectByDistrictId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict subcontractProfileDistrict)
        {
            var p = new DynamicParameters();

            p.Add("@district_id", subcontractProfileDistrict.DistrictId);
            p.Add("@district_name", subcontractProfileDistrict.DistrictName);
            p.Add("@province_id", subcontractProfileDistrict.ProvinceId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileDistrict_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict subcontractProfileDistrict)
        {
            var p = new DynamicParameters();
            p.Add("@district_id", subcontractProfileDistrict.DistrictId);
            p.Add("@district_name", subcontractProfileDistrict.DistrictName);
            p.Add("@province_id", subcontractProfileDistrict.ProvinceId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileDistrict_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(int districtId)
        {
            var p = new DynamicParameters();
            p.Add("@district_id", districtId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileDistrict_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict> subcontractProfileDistrictList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileDistrictDataTable(subcontractProfileDistrictList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileDistrict_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileDistrictDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict> SubcontractProfileDistrictList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("district_id", typeof(SqlInt32));
            dt.Columns.Add("district_name", typeof(SqlString));
            dt.Columns.Add("province_id", typeof(SqlInt32));

            if (SubcontractProfileDistrictList != null)
                foreach (var curObj in SubcontractProfileDistrictList)
                {
                    DataRow row = dt.NewRow();
                    row["district_id"] = new SqlInt32((int)curObj.DistrictId);
                    row["district_name"] = new SqlString(curObj.DistrictName);
                    row["province_id"] = new SqlInt32((int)curObj.ProvinceId);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileDistrictPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict>
                ("uspSubcontractProfileDistrict_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileDistrictPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileDistrict_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("district_id", typeof(SqlInt32));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["district_id"] = new SqlInt32((int)curObj.DistrictId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }
}
