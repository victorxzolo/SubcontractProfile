using SubcontractProfile.WebApi.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using Repository;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Data.SqlTypes;
using SubcontractProfile.WebApi.Services.Model;

namespace SubcontractProfile.WebApi.Services.Services
{

    /// =================================================================
    /// Author: AIS-X10
    /// Description:	Class for the repo SubcontractProfileTrainingEngineerRepo 
    /// =================================================================
    public partial class SubcontractProfileTrainingEngineerRepo : ISubcontractProfileTrainingEngineerRepo
    {

        private IDbContext _dbContext;

        public SubcontractProfileTrainingEngineerRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer>
            ("uspSubcontractProfileTrainingEngineer_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer> GetByTrainingEngineerId(System.Guid trainingEngineerId)
        {
            var p = new DynamicParameters();
            p.Add("@training_engineer_id", trainingEngineerId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer>
            ("uspSubcontractProfileTrainingEngineer_selectByTrainingEngineerId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer subcontractProfileTrainingEngineer)
        {
            var p = new DynamicParameters();

            p.Add("@training_id", subcontractProfileTrainingEngineer.TrainingId);
            //p.Add("@location_id", subcontractProfileTrainingEngineer.LocationId);
            //p.Add("@team_id", subcontractProfileTrainingEngineer.TeamId);
            p.Add("@engineer_id", subcontractProfileTrainingEngineer.EngineerId);
            p.Add("@create_by", subcontractProfileTrainingEngineer.CreateBy);
            p.Add("@course_price", subcontractProfileTrainingEngineer.CoursePrice); 

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTrainingEngineer_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer subcontractProfileTrainingEngineer)
        {
            var p = new DynamicParameters();
            p.Add("@training_engineer_id", subcontractProfileTrainingEngineer.TrainingEngineerId);
            p.Add("@training_id", subcontractProfileTrainingEngineer.TrainingId);
            p.Add("@engineer_id", subcontractProfileTrainingEngineer.EngineerId);
            p.Add("@test_status", subcontractProfileTrainingEngineer.TestStatus);
            p.Add("@update_by", subcontractProfileTrainingEngineer.UpdateBy);
            p.Add("@course_price", subcontractProfileTrainingEngineer.CoursePrice);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTrainingEngineer_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(System.Guid trainingEngineerId)
        {
            var p = new DynamicParameters();
            p.Add("@training_engineer_id", trainingEngineerId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTrainingEngineer_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> DeleteByTriningId(System.Guid trainingId)
        {
            var p = new DynamicParameters();
            p.Add("@training_id", trainingId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTrainingEngineerByTrainingId_delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer> subcontractProfileTrainingEngineerList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileTrainingEngineerDataTable(subcontractProfileTrainingEngineerList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTrainingEngineer_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileTrainingEngineerDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer> SubcontractProfileTrainingEngineerList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("training_engineer_id", typeof(SqlGuid));
            dt.Columns.Add("training_id", typeof(SqlGuid));
            dt.Columns.Add("location_id", typeof(SqlGuid));
            dt.Columns.Add("team_id", typeof(SqlGuid));
            dt.Columns.Add("engineer_id", typeof(SqlGuid));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("create_by", typeof(SqlString));

            if (SubcontractProfileTrainingEngineerList != null)
                foreach (var curObj in SubcontractProfileTrainingEngineerList)
                {
                    DataRow row = dt.NewRow();
                  //  row["training_engineer_id"] = new SqlGuid(curObj.TrainingEngineerId);
                    row["training_id"] = new SqlGuid(curObj.TrainingId);
                    row["location_id"] = new SqlGuid(curObj.LocationId);
                    row["team_id"] = new SqlGuid(curObj.TeamId);
                    row["engineer_id"] = new SqlGuid(curObj.EngineerId);
                  //  row["create_date"] = curObj.CreateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.CreateDate.Value);
                    row["create_by"] = new SqlString(curObj.CreateBy);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileTrainingEngineerPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer>
                ("uspSubcontractProfileTrainingEngineer_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileTrainingEngineerPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("training_engineer_id", typeof(SqlGuid));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["training_engineer_id"] = new SqlGuid(curObj.TrainingEngineerId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        public async Task<IEnumerable<SubcontractProfileTrainingEngineer>> GetTrainingEngineerByTrainingId(Guid training_Id)
        {
            var p = new DynamicParameters();
            p.Add("@training_id", training_Id);

            var entity = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileTrainingEngineer>
            ("uspSubcontractProfileTrainingEngineer_selectTrainingEngineerByTrainingId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        public async Task<bool> UpdateByTestResult(SubcontractProfileTrainingEngineer subcontractProfileTrainingEngineer)
        {
            var p = new DynamicParameters();
   
            p.Add("@training_id", subcontractProfileTrainingEngineer.TrainingId);
            p.Add("@location_id", subcontractProfileTrainingEngineer.LocationId);
            p.Add("@team_id", subcontractProfileTrainingEngineer.TeamId);
            p.Add("@engineer_id", subcontractProfileTrainingEngineer.EngineerId);
            p.Add("@test_status", subcontractProfileTrainingEngineer.TestStatus);
            p.Add("@update_by", subcontractProfileTrainingEngineer.UpdateBy);
            p.Add("@testreason", subcontractProfileTrainingEngineer.TestReason);
            p.Add("@testremark", subcontractProfileTrainingEngineer.TestRemark);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileTrainingEngineer_updateByTest", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }
    }
}
