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
    /// Description:	Class for the repo SubcontractProfileWarrantyRepo 
    /// =================================================================
    public partial class SubcontractProfileWarrantyRepo : ISubcontractProfileWarrantyRepo
    {

        protected Repository.DbContext _dbContext = null;

        public SubcontractProfileWarrantyRepo(Repository.DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty>
            ("uspSubcontractProfileWarranty_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty> GetByWarrantyId(string warrantyId)
        {
            var p = new DynamicParameters();
            p.Add("@warranty_id", warrantyId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty>
            ("uspSubcontractProfileWarranty_selectByWarrantyId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty subcontractProfileWarranty)
        {
            var p = new DynamicParameters();

            p.Add("@warranty_id", subcontractProfileWarranty.WarrantyId);
            p.Add("@warranty_desc", subcontractProfileWarranty.WarrantyDesc);
            p.Add("@create_date", subcontractProfileWarranty.CreateDate);
            p.Add("@create_by", subcontractProfileWarranty.CreateBy);
            p.Add("@modifiled_by", subcontractProfileWarranty.ModifiledBy);
            p.Add("@modifiled_date", subcontractProfileWarranty.ModifiledDate);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileWarranty_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty subcontractProfileWarranty)
        {
            var p = new DynamicParameters();
            p.Add("@warranty_id", subcontractProfileWarranty.WarrantyId);
            p.Add("@warranty_desc", subcontractProfileWarranty.WarrantyDesc);
            p.Add("@create_date", subcontractProfileWarranty.CreateDate);
            p.Add("@create_by", subcontractProfileWarranty.CreateBy);
            p.Add("@modifiled_by", subcontractProfileWarranty.ModifiledBy);
            p.Add("@modifiled_date", subcontractProfileWarranty.ModifiledDate);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileWarranty_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string warrantyId)
        {
            var p = new DynamicParameters();
            p.Add("@warranty_id", warrantyId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileWarranty_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty> subcontractProfileWarrantyList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileWarrantyDataTable(subcontractProfileWarrantyList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileWarranty_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileWarrantyDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty> SubcontractProfileWarrantyList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("warranty_id", typeof(SqlString));
            dt.Columns.Add("warranty_desc", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("create_by", typeof(SqlString));
            dt.Columns.Add("modifiled_by", typeof(SqlString));
            dt.Columns.Add("modifiled_date", typeof(SqlDateTime));

            if (SubcontractProfileWarrantyList != null)
                foreach (var curObj in SubcontractProfileWarrantyList)
                {
                    DataRow row = dt.NewRow();
                    row["warranty_id"] = new SqlString(curObj.WarrantyId);
                    row["warranty_desc"] = new SqlString(curObj.WarrantyDesc);
                    row["create_date"] = curObj.CreateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.CreateDate.Value);
                    row["create_by"] = new SqlString(curObj.CreateBy);
                    row["modifiled_by"] = new SqlString(curObj.ModifiledBy);
                    row["modifiled_date"] = curObj.ModifiledDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.ModifiledDate.Value);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileWarrantyPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty>
                ("uspSubcontractProfileWarranty_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileWarrantyPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileWarranty_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("warranty_id", typeof(SqlString));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["warranty_id"] = new SqlString(curObj.WarrantyId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }

}
