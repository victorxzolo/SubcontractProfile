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
    /// Description:	Class for the repo SubcontractProfileUserRepo 
    /// =================================================================
    public partial class SubcontractProfileUserRepo : ISubcontractProfileUserRepo
    {

        protected Repository.DbContext _dbContext = null;

        public SubcontractProfileUserRepo(Repository.DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser>
            ("uspSubcontractProfileUser_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser> GetByUserId(System.Guid userId)
        {
            var p = new DynamicParameters();
            p.Add("@user_id", userId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser>
            ("uspSubcontractProfileUser_selectByUserId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser subcontractProfileUser)
        {
            var p = new DynamicParameters();

            p.Add("@user_id", subcontractProfileUser.UserId);
            p.Add("@username", subcontractProfileUser.Username);
            p.Add("@sub_module_name", subcontractProfileUser.SubModuleName);
            p.Add("@sso_first_name", subcontractProfileUser.SsoFirstName);
            p.Add("@sso_last_name", subcontractProfileUser.SsoLastName);
            p.Add("@staff_name", subcontractProfileUser.StaffName);
            p.Add("@staff_role", subcontractProfileUser.StaffRole);
            p.Add("@create_date", subcontractProfileUser.CreateDate);
            p.Add("@create_by", subcontractProfileUser.CreateBy);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileUser_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser subcontractProfileUser)
        {
            var p = new DynamicParameters();
            p.Add("@user_id", subcontractProfileUser.UserId);
            p.Add("@username", subcontractProfileUser.Username);
            p.Add("@sub_module_name", subcontractProfileUser.SubModuleName);
            p.Add("@sso_first_name", subcontractProfileUser.SsoFirstName);
            p.Add("@sso_last_name", subcontractProfileUser.SsoLastName);
            p.Add("@staff_name", subcontractProfileUser.StaffName);
            p.Add("@staff_role", subcontractProfileUser.StaffRole);
            p.Add("@create_date", subcontractProfileUser.CreateDate);
            p.Add("@create_by", subcontractProfileUser.CreateBy);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileUser_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string userId)
        {
            var p = new DynamicParameters();
            p.Add("@user_id", userId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileUser_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser> subcontractProfileUserList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileUserDataTable(subcontractProfileUserList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileUser_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileUserDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser> SubcontractProfileUserList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("user_id", typeof(SqlGuid));
            dt.Columns.Add("username", typeof(SqlString));
            dt.Columns.Add("sub_module_name", typeof(SqlString));
            dt.Columns.Add("sso_first_name", typeof(SqlString));
            dt.Columns.Add("sso_last_name", typeof(SqlString));
            dt.Columns.Add("staff_name", typeof(SqlString));
            dt.Columns.Add("staff_role", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("create_by", typeof(SqlString));

            if (SubcontractProfileUserList != null)
                foreach (var curObj in SubcontractProfileUserList)
                {
                    DataRow row = dt.NewRow();
                    row["user_id"] = new SqlGuid(curObj.UserId);
                    row["username"] = new SqlString(curObj.Username);
                    row["sub_module_name"] = new SqlString(curObj.SubModuleName);
                    row["sso_first_name"] = new SqlString(curObj.SsoFirstName);
                    row["sso_last_name"] = new SqlString(curObj.SsoLastName);
                    row["staff_name"] = new SqlString(curObj.StaffName);
                    row["staff_role"] = new SqlString(curObj.StaffRole);
                    row["create_date"] = curObj.CreateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.CreateDate.Value);
                    row["create_by"] = new SqlString(curObj.CreateBy);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileUserPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser>
                ("uspSubcontractProfileUser_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileUserPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileUser_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("user_id", typeof(SqlGuid));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["user_id"] = new SqlGuid(curObj.UserId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }
}
