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
    /// Description:	Class for the repo SubcontractProfileVerhicleTypeRepo 
    /// =================================================================
    public partial class SubcontractProfileVerhicleTypeRepo : ISubcontractProfileVerhicleTypeRepo
    {

        protected IDbContext _dbContext;

        public SubcontractProfileVerhicleTypeRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType>
            ("uspSubcontractProfileVerhicleType_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType> GetByVerhicleTypeId(string verhicleTypeId)
        {
            var p = new DynamicParameters();
            p.Add("@verhicle_type_id", verhicleTypeId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType>
            ("uspSubcontractProfileVerhicleType_selectByVerhicleTypeId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType subcontractProfileVerhicleType)
        {
            var p = new DynamicParameters();

            p.Add("@verhicle_type_id", subcontractProfileVerhicleType.VerhicleTypeId);
            p.Add("@verhicl_type_name", subcontractProfileVerhicleType.VerhiclTypeName);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleType_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType subcontractProfileVerhicleType)
        {
            var p = new DynamicParameters();
            p.Add("@verhicle_type_id", subcontractProfileVerhicleType.VerhicleTypeId);
            p.Add("@verhicl_type_name", subcontractProfileVerhicleType.VerhiclTypeName);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleType_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string verhicleTypeId)
        {
            var p = new DynamicParameters();
            p.Add("@verhicle_type_id", verhicleTypeId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleType_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType> subcontractProfileVerhicleTypeList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileVerhicleTypeDataTable(subcontractProfileVerhicleTypeList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileVerhicleType_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileVerhicleTypeDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType> SubcontractProfileVerhicleTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("verhicle_type_id", typeof(SqlString));
            dt.Columns.Add("verhicl_type_name", typeof(SqlString));

            if (SubcontractProfileVerhicleTypeList != null)
                foreach (var curObj in SubcontractProfileVerhicleTypeList)
                {
                    DataRow row = dt.NewRow();
                    row["verhicle_type_id"] = new SqlString(curObj.VerhicleTypeId);
                    row["verhicl_type_name"] = new SqlString(curObj.VerhiclTypeName);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileVerhicleTypePKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType>
                ("uspSubcontractProfileVerhicleType_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileVerhicleTypePKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileVerhicleType_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("verhicle_type_id", typeof(SqlString));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["verhicle_type_id"] = new SqlString(curObj.VerhicleTypeId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }
}
