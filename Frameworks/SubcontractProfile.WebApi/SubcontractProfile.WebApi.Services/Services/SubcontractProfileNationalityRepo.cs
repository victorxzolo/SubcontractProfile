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
    /// Description:	Class for the repo SubcontractProfileNationalityRepo 
    /// =================================================================
    public partial class SubcontractProfileNationalityRepo : ISubcontractProfileNationalityRepo
    {

        protected Repository.DbContext _dbContext = null;

        public SubcontractProfileNationalityRepo(Repository.DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality>
            ("uspSubcontractProfileNationality_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality> GetByNationalityId(string nationalityId)
        {
            var p = new DynamicParameters();
            p.Add("@nationality_id", nationalityId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality>
            ("uspSubcontractProfileNationality_selectByNationalityId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality subcontractProfileNationality)
        {
            var p = new DynamicParameters();

            p.Add("@nationality_id", subcontractProfileNationality.NationalityId);
            p.Add("@nationality_th", subcontractProfileNationality.NationalityTh);
            p.Add("@nationality_en", subcontractProfileNationality.NationalityEn);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileNationality_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality subcontractProfileNationality)
        {
            var p = new DynamicParameters();
            p.Add("@nationality_id", subcontractProfileNationality.NationalityId);
            p.Add("@nationality_th", subcontractProfileNationality.NationalityTh);
            p.Add("@nationality_en", subcontractProfileNationality.NationalityEn);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileNationality_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string nationalityId)
        {
            var p = new DynamicParameters();
            p.Add("@nationality_id", nationalityId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileNationality_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality> subcontractProfileNationalityList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileNationalityDataTable(subcontractProfileNationalityList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileNationality_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileNationalityDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality> SubcontractProfileNationalityList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nationality_id", typeof(SqlString));
            dt.Columns.Add("nationality_th", typeof(SqlString));
            dt.Columns.Add("nationality_en", typeof(SqlString));

            if (SubcontractProfileNationalityList != null)
                foreach (var curObj in SubcontractProfileNationalityList)
                {
                    DataRow row = dt.NewRow();
                    row["nationality_id"] = new SqlString(curObj.NationalityId);
                    row["nationality_th"] = new SqlString(curObj.NationalityTh);
                    row["nationality_en"] = new SqlString(curObj.NationalityEn);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileNationalityPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality>
                ("uspSubcontractProfileNationality_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileNationalityPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileNationality_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("nationality_id", typeof(SqlString));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["nationality_id"] = new SqlString(curObj.NationalityId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }

}
