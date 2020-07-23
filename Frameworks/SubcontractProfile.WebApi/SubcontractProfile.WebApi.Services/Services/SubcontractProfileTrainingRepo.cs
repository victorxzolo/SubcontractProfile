﻿using Dapper;
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

        protected Repository.DbContext _dbContext = null;

        public SubcontractProfileTrainingRepo(Repository.DbContext dbContext)
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
            p.Add("@create_by", subcontractProfileTraining.CreateBy);
            p.Add("@create_date", subcontractProfileTraining.CreateDate);
            p.Add("@modified_by", subcontractProfileTraining.ModifiedBy);
            p.Add("@modified_date", subcontractProfileTraining.ModifiedDate);
            p.Add("@total_price", subcontractProfileTraining.TotalPrice);
            p.Add("@vat", subcontractProfileTraining.Vat);
            p.Add("@tax", subcontractProfileTraining.Tax);
            p.Add("@status", subcontractProfileTraining.Status);
            p.Add("@request_no", subcontractProfileTraining.RequestNo);

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
            p.Add("@create_by", subcontractProfileTraining.CreateBy);
            p.Add("@create_date", subcontractProfileTraining.CreateDate);
            p.Add("@modified_by", subcontractProfileTraining.ModifiedBy);
            p.Add("@modified_date", subcontractProfileTraining.ModifiedDate);
            p.Add("@total_price", subcontractProfileTraining.TotalPrice);
            p.Add("@vat", subcontractProfileTraining.Vat);
            p.Add("@tax", subcontractProfileTraining.Tax);
            p.Add("@status", subcontractProfileTraining.Status);
            p.Add("@request_no", subcontractProfileTraining.RequestNo);

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
            dt.Columns.Add("create_by", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("modified_by", typeof(SqlString));
            dt.Columns.Add("modified_date", typeof(SqlDateTime));
            dt.Columns.Add("total_price", typeof(SqlDecimal));
            dt.Columns.Add("vat", typeof(SqlDecimal));
            dt.Columns.Add("tax", typeof(SqlDecimal));
            dt.Columns.Add("status", typeof(SqlString));
            dt.Columns.Add("request_no", typeof(SqlString));

            if (SubcontractProfileTrainingList != null)
                foreach (var curObj in SubcontractProfileTrainingList)
                {
                    DataRow row = dt.NewRow();
                    row["training_id"] = new SqlGuid(curObj.TrainingId);
                    row["company_id"] = new SqlGuid(curObj.CompanyId);
                    row["course"] = new SqlString(curObj.Course);
                    row["request_date"] = curObj.RequestDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.RequestDate.Value);
                    row["remark"] = new SqlString(curObj.Remark);
                    row["create_by"] = new SqlString(curObj.CreateBy);
                    row["create_date"] = curObj.CreateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.CreateDate.Value);
                    row["modified_by"] = new SqlString(curObj.ModifiedBy);
                    row["modified_date"] = curObj.ModifiedDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.ModifiedDate.Value);
                    row["total_price"] = curObj.TotalPrice == null ? SqlDecimal.Null : new SqlDecimal(curObj.TotalPrice.Value);
                    row["vat"] = curObj.Vat == null ? SqlDecimal.Null : new SqlDecimal(curObj.Vat.Value);
                    row["tax"] = curObj.Tax == null ? SqlDecimal.Null : new SqlDecimal(curObj.Tax.Value);
                    row["status"] = new SqlString(curObj.Status);
                    row["request_no"] = new SqlString(curObj.RequestNo);

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

    }
}
