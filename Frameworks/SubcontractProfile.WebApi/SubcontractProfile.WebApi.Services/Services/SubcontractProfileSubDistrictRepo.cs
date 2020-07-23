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
    /// Description:	Class for the repo SubcontractProfileSubDistrictRepo 
    /// =================================================================
    public partial class SubcontractProfileSubDistrictRepo : ISubcontractProfileSubDistrictRepo
    {

        protected Repository.DbContext _dbContext = null;

        public SubcontractProfileSubDistrictRepo(Repository.DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict>
            ("uspSubcontractProfileSubDistrict_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict> GetBySubDistrictId(int subDistrictId)
        {
            var p = new DynamicParameters();
            p.Add("@sub_district_id", subDistrictId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict>
            ("uspSubcontractProfileSubDistrict_selectBySubDistrictId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict subcontractProfileSubDistrict)
        {
            var p = new DynamicParameters();

            p.Add("@sub_district_id", subcontractProfileSubDistrict.SubDistrictId);
            p.Add("@sub_district_name", subcontractProfileSubDistrict.SubDistrictName);
            p.Add("@zip_code", subcontractProfileSubDistrict.ZipCode);
            p.Add("@district_id", subcontractProfileSubDistrict.DistrictId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileSubDistrict_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict subcontractProfileSubDistrict)
        {
            var p = new DynamicParameters();
            p.Add("@sub_district_id", subcontractProfileSubDistrict.SubDistrictId);
            p.Add("@sub_district_name", subcontractProfileSubDistrict.SubDistrictName);
            p.Add("@zip_code", subcontractProfileSubDistrict.ZipCode);
            p.Add("@district_id", subcontractProfileSubDistrict.DistrictId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileSubDistrict_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string subDistrictId)
        {
            var p = new DynamicParameters();
            p.Add("@sub_district_id", subDistrictId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileSubDistrict_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict> subcontractProfileSubDistrictList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileSubDistrictDataTable(subcontractProfileSubDistrictList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileSubDistrict_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileSubDistrictDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict> SubcontractProfileSubDistrictList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("sub_district_id", typeof(SqlInt32));
            dt.Columns.Add("sub_district_name", typeof(SqlString));
            dt.Columns.Add("zip_code", typeof(SqlString));
            dt.Columns.Add("district_id", typeof(SqlInt32));

            if (SubcontractProfileSubDistrictList != null)
                foreach (var curObj in SubcontractProfileSubDistrictList)
                {
                    DataRow row = dt.NewRow();
                    row["sub_district_id"] = new SqlInt32((int)curObj.SubDistrictId);
                    row["sub_district_name"] = new SqlString(curObj.SubDistrictName);
                    row["zip_code"] = new SqlString(curObj.ZipCode);
                    row["district_id"] = new SqlInt32((int)curObj.DistrictId);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileSubDistrictPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict>
                ("uspSubcontractProfileSubDistrict_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileSubDistrictPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileSubDistrict_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("sub_district_id", typeof(SqlInt32));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["sub_district_id"] = new SqlInt32((int)curObj.SubDistrictId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }


}
