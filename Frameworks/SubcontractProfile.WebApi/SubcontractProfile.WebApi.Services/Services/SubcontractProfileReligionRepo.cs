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
    /// Description:	Class for the repo SubcontractProfileReligionRepo 
    /// =================================================================
    public partial class SubcontractProfileReligionRepo : ISubcontractProfileReligionRepo
    {

        protected IDbContext _dbContext;

        public SubcontractProfileReligionRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion>
            ("uspSubcontractProfileReligion_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion> GetByReligionId(string religionId)
        {
            var p = new DynamicParameters();
            p.Add("@religion_id", religionId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion>
            ("uspSubcontractProfileReligion_selectByReligionId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion subcontractProfileReligion)
        {
            var p = new DynamicParameters();

            p.Add("@religion_id", subcontractProfileReligion.ReligionId);
            p.Add("@religion_name_th", subcontractProfileReligion.ReligionNameTh);
            p.Add("@religion_name_en", subcontractProfileReligion.ReligionNameEn);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileReligion_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion subcontractProfileReligion)
        {
            var p = new DynamicParameters();
            p.Add("@religion_id", subcontractProfileReligion.ReligionId);
            p.Add("@religion_name_th", subcontractProfileReligion.ReligionNameTh);
            p.Add("@religion_name_en", subcontractProfileReligion.ReligionNameEn);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileReligion_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string religionId)
        {
            var p = new DynamicParameters();
            p.Add("@religion_id", religionId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileReligion_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion> subcontractProfileReligionList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileReligionDataTable(subcontractProfileReligionList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileReligion_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileReligionDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion> SubcontractProfileReligionList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("religion_id", typeof(SqlString));
            dt.Columns.Add("religion_name_th", typeof(SqlString));
            dt.Columns.Add("religion_name_en", typeof(SqlString));

            if (SubcontractProfileReligionList != null)
                foreach (var curObj in SubcontractProfileReligionList)
                {
                    DataRow row = dt.NewRow();
                    row["religion_id"] = new SqlString(curObj.ReligionId);
                    row["religion_name_th"] = new SqlString(curObj.ReligionNameTh);
                    row["religion_name_en"] = new SqlString(curObj.ReligionNameEn);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileReligionPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion>
                ("uspSubcontractProfileReligion_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileReligionPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileReligion_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("religion_id", typeof(SqlString));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["religion_id"] = new SqlString(curObj.ReligionId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }
}
