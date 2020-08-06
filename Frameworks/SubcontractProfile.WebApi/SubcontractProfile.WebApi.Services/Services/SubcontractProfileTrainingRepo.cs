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
    /// Description:	Class for the repo SubcontractProfileTrainingRepo 
    /// =================================================================
    public partial class SubcontractProfileTrainingRepo : ISubcontractProfileTrainingRepo
    {

        protected IDbContext _dbContext;

        public SubcontractProfileTrainingRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }



        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>
            ("uspSubcontractProfileTraining_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining> GetByTrainingId(System.Guid trainingId)
        {
            var p = new DynamicParameters();
            p.Add("@training_id", trainingId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>
            ("uspSubcontractProfileTraining_selectByTrainingId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining subcontractProfileTraining)
        {
            var p = new DynamicParameters();

            p.Add("@training_id", subcontractProfileTraining.TrainingId);
            p.Add("@company_id", subcontractProfileTraining.CompanyId);
            p.Add("@course", subcontractProfileTraining.Course);
            p.Add("@request_date", subcontractProfileTraining.RequestDate);
            p.Add("@remark", subcontractProfileTraining.Remark);
            p.Add("@total_price", subcontractProfileTraining.TotalPrice);
            p.Add("@vat", subcontractProfileTraining.Vat);
            p.Add("@tax", subcontractProfileTraining.Tax);
            p.Add("@status", subcontractProfileTraining.Status);
            p.Add("@request_no", subcontractProfileTraining.RequestNo);
            p.Add("@engineer_id", subcontractProfileTraining.EngineerId);
            p.Add("@team_id", subcontractProfileTraining.TeamId);
            p.Add("@location_id", subcontractProfileTraining.LocationId);
            p.Add("@create_by", subcontractProfileTraining.CreateBy);
            p.Add("@create_date", subcontractProfileTraining.CreateDate);
            p.Add("@modified_by", subcontractProfileTraining.ModifiedBy);
            p.Add("@modified_date", subcontractProfileTraining.ModifiedDate);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTraining_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining subcontractProfileTraining)
        {
            var p = new DynamicParameters();
            p.Add("@training_id", subcontractProfileTraining.TrainingId);
            p.Add("@company_id", subcontractProfileTraining.CompanyId);
            p.Add("@course", subcontractProfileTraining.Course);
            p.Add("@request_date", subcontractProfileTraining.RequestDate);
            p.Add("@remark", subcontractProfileTraining.Remark);
            p.Add("@total_price", subcontractProfileTraining.TotalPrice);
            p.Add("@vat", subcontractProfileTraining.Vat);
            p.Add("@tax", subcontractProfileTraining.Tax);
            p.Add("@status", subcontractProfileTraining.Status);
            p.Add("@request_no", subcontractProfileTraining.RequestNo);
            p.Add("@engineer_id", subcontractProfileTraining.EngineerId);
            p.Add("@team_id", subcontractProfileTraining.TeamId);
            p.Add("@location_id", subcontractProfileTraining.LocationId);
            p.Add("@create_by", subcontractProfileTraining.CreateBy);
            p.Add("@create_date", subcontractProfileTraining.CreateDate);
            p.Add("@modified_by", subcontractProfileTraining.ModifiedBy);
            p.Add("@modified_date", subcontractProfileTraining.ModifiedDate);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTraining_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string trainingId)
        {
            var p = new DynamicParameters();
            p.Add("@training_id", trainingId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTraining_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining> subcontractProfileTrainingList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileTrainingDataTable(subcontractProfileTrainingList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTraining_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileTrainingDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining> SubcontractProfileTrainingList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("training_id", typeof(SqlGuid));
            dt.Columns.Add("company_id", typeof(SqlGuid));
            dt.Columns.Add("course", typeof(SqlString));
            dt.Columns.Add("request_date", typeof(SqlDateTime));
            dt.Columns.Add("remark", typeof(SqlString));
            dt.Columns.Add("total_price", typeof(SqlDecimal));
            dt.Columns.Add("vat", typeof(SqlDecimal));
            dt.Columns.Add("tax", typeof(SqlDecimal));
            dt.Columns.Add("status", typeof(SqlString));
            dt.Columns.Add("request_no", typeof(SqlString));
            dt.Columns.Add("engineer_id", typeof(SqlString));
            dt.Columns.Add("team_id", typeof(SqlString));
            dt.Columns.Add("location_id", typeof(SqlString));
            dt.Columns.Add("create_by", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("modified_by", typeof(SqlString));
            dt.Columns.Add("modified_date", typeof(SqlDateTime));

            if (SubcontractProfileTrainingList != null)
                foreach (var curObj in SubcontractProfileTrainingList)
                {
                    DataRow row = dt.NewRow();
                    row["training_id"] = new SqlGuid(curObj.TrainingId);
                    row["company_id"] = new SqlGuid(curObj.CompanyId);
                    row["course"] = new SqlString(curObj.Course);
                    row["request_date"] = new SqlDateTime(curObj.RequestDate);
                    row["remark"] = new SqlString(curObj.Remark);
                    row["total_price"] = new SqlDecimal(curObj.TotalPrice);
                    row["vat"] = new SqlDecimal(curObj.Vat);
                    row["tax"] = curObj.Tax == null ? SqlDecimal.Null : new SqlDecimal(curObj.Tax.Value);
                    row["status"] = new SqlString(curObj.Status);
                    row["request_no"] = new SqlString(curObj.RequestNo);
                    row["engineer_id"] = new SqlString(curObj.EngineerId);
                    row["team_id"] = new SqlString(curObj.TeamId);
                    row["location_id"] = new SqlString(curObj.LocationId);
                    row["create_by"] = new SqlString(curObj.CreateBy);
                    row["create_date"] = curObj.CreateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.CreateDate.Value);
                    row["modified_by"] = new SqlString(curObj.ModifiedBy);
                    row["modified_date"] = curObj.ModifiedDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.ModifiedDate.Value);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileTrainingPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>
                ("uspSubcontractProfileTraining_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileTrainingPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("training_id", typeof(SqlGuid));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["training_id"] = new SqlGuid(curObj.TrainingId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>> SearchTraining(string company_id, string location_code,
            string team_id, string staff_name_th, string position_id, string status,
            string date_from, string date_to)
        {
            var p = new DynamicParameters();
            p.Add("@company_id", company_id);
            p.Add("@location_code", location_code);
            p.Add("@team_id", team_id);
            p.Add("@staff_name_th", staff_name_th);
            p.Add("@position_id", position_id);
            p.Add("@status", status);
            p.Add("@date_from", date_from);
            p.Add("@date_to", date_to);


            var entity = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTraining>
            ("uspSubcontractProfileTraining_searchTraining", p, commandType: CommandType.StoredProcedure);

            return entity;
        }
    }


}
