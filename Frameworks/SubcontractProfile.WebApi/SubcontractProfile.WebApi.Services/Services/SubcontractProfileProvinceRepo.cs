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
    /// Description:	Class for the repo SubcontractProfileProvinceRepo 
    /// =================================================================
    public partial class SubcontractProfileProvinceRepo : ISubcontractProfileProvinceRepo
    {

        protected Repository.DbContext _dbContext = null;

        public SubcontractProfileProvinceRepo(Repository.DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince>
            ("uspSubcontractProfileProvince_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince> GetByProvinceId(int provinceId)
        {
            var p = new DynamicParameters();
            p.Add("@province_id", provinceId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince>
            ("uspSubcontractProfileProvince_selectByProvinceId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince subcontractProfileProvince)
        {
            var p = new DynamicParameters();

            p.Add("@province_id", subcontractProfileProvince.ProvinceId);
            p.Add("@province_name", subcontractProfileProvince.ProvinceName);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileProvince_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince subcontractProfileProvince)
        {
            var p = new DynamicParameters();
            p.Add("@province_id", subcontractProfileProvince.ProvinceId);
            p.Add("@province_name", subcontractProfileProvince.ProvinceName);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileProvince_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string provinceId)
        {
            var p = new DynamicParameters();
            p.Add("@province_id", provinceId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileProvince_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince> subcontractProfileProvinceList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileProvinceDataTable(subcontractProfileProvinceList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileProvince_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileProvinceDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince> SubcontractProfileProvinceList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("province_id", typeof(SqlInt32));
            dt.Columns.Add("province_name", typeof(SqlString));

            if (SubcontractProfileProvinceList != null)
                foreach (var curObj in SubcontractProfileProvinceList)
                {
                    DataRow row = dt.NewRow();
                    row["province_id"] = new SqlInt32((int)curObj.ProvinceId);
                    row["province_name"] = new SqlString(curObj.ProvinceName);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileProvincePKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince>
                ("uspSubcontractProfileProvince_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileProvincePKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileProvince_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("province_id", typeof(SqlInt32));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["province_id"] = new SqlInt32((int)curObj.ProvinceId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }
}
