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
    /// Description:	Class for the repo SubcontractProfileCompanyTypeRepo 
    /// =================================================================
    public partial class SubcontractProfileCompanyTypeRepo : ISubcontractProfileCompanyTypeRepo
    {
        private IDbContext _dbContext;

        public SubcontractProfileCompanyTypeRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType>
            ("uspSubcontractProfileCompanyType_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType> GetByCompanyTypeId(string companyTypeId)
        {
            var p = new DynamicParameters();
            p.Add("@company_type_id", companyTypeId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType>
            ("uspSubcontractProfileCompanyType_selectByCompanyTypeId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType subcontractProfileCompanyType)
        {
            var p = new DynamicParameters();

            p.Add("@company_type_id", subcontractProfileCompanyType.CompanyTypeId);
            p.Add("@company_type_name_th", subcontractProfileCompanyType.CompanyTypeNameTh);
            p.Add("@company_type_name_en", subcontractProfileCompanyType.CompanyTypeNameEn);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileCompanyType_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType subcontractProfileCompanyType)
        {
            var p = new DynamicParameters();
            p.Add("@company_type_id", subcontractProfileCompanyType.CompanyTypeId);
            p.Add("@company_type_name_th", subcontractProfileCompanyType.CompanyTypeNameTh);
            p.Add("@company_type_name_en", subcontractProfileCompanyType.CompanyTypeNameEn);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileCompanyType_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string companyTypeId)
        {
            var p = new DynamicParameters();
            p.Add("@company_type_id", companyTypeId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileCompanyType_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType> subcontractProfileCompanyTypeList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileCompanyTypeDataTable(subcontractProfileCompanyTypeList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileCompanyType_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileCompanyTypeDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType> SubcontractProfileCompanyTypeList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("company_type_id", typeof(SqlString));
            dt.Columns.Add("company_type_name_th", typeof(SqlString));
            dt.Columns.Add("company_type_name_en", typeof(SqlString));

            if (SubcontractProfileCompanyTypeList != null)
                foreach (var curObj in SubcontractProfileCompanyTypeList)
                {
                    DataRow row = dt.NewRow();
                    row["company_type_id"] = new SqlString(curObj.CompanyTypeId);
                    row["company_type_name_th"] = new SqlString(curObj.CompanyTypeNameTh);
                    row["company_type_name_en"] = new SqlString(curObj.CompanyTypeNameEn);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileCompanyTypePKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType>
                ("uspSubcontractProfileCompanyType_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileCompanyTypePKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileCompanyType_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("company_type_id", typeof(SqlString));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["company_type_id"] = new SqlString(curObj.CompanyTypeId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }
}

