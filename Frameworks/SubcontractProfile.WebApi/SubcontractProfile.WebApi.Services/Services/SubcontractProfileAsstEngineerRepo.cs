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
    /// Description:	Class for the repo SubcontractProfileAsstEngineerRepo 
    /// =================================================================
    public partial class SubcontractProfileAsstEngineerRepo : ISubcontractProfileAsstEngineerRepo
    {

        private IDbContext _dbContext;

        public SubcontractProfileAsstEngineerRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer>
            ("uspSubcontractProfileAsstEngineer_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer> GetByAsstEngineerId(System.Guid asstEngineerId)
        {
            var p = new DynamicParameters();
            p.Add("@asst_engineer_id", asstEngineerId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer>
            ("uspSubcontractProfileAsstEngineer_selectByAsstEngineerId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer subcontractProfileAsstEngineer)
        {
            var p = new DynamicParameters();

            p.Add("@asst_engineer_id", subcontractProfileAsstEngineer.AsstEngineerId);
            p.Add("@staff_code", subcontractProfileAsstEngineer.StaffCode);
            p.Add("@staff_name", subcontractProfileAsstEngineer.StaffName);
            p.Add("@asc_code", subcontractProfileAsstEngineer.AscCode);
            p.Add("@tshirt_size", subcontractProfileAsstEngineer.TshirtSize);
            p.Add("@contract_phone1", subcontractProfileAsstEngineer.ContractPhone1);
            p.Add("@contract_phone2", subcontractProfileAsstEngineer.ContractPhone2);
            p.Add("@contract_email", subcontractProfileAsstEngineer.ContractEmail);
            p.Add("@work_experience", subcontractProfileAsstEngineer.WorkExperience);
            p.Add("@work_experience_attach_file", subcontractProfileAsstEngineer.WorkExperienceAttachFile);
            p.Add("@work_type", subcontractProfileAsstEngineer.WorkType);
            p.Add("@course_skill", subcontractProfileAsstEngineer.CourseSkill);
            p.Add("@skill_level", subcontractProfileAsstEngineer.SkillLevel);
            p.Add("@vehicle_type", subcontractProfileAsstEngineer.VehicleType);
            p.Add("@vehicle_brand", subcontractProfileAsstEngineer.VehicleBrand);
            p.Add("@vehicle_serise", subcontractProfileAsstEngineer.VehicleSerise);
            p.Add("@vehicle_color", subcontractProfileAsstEngineer.VehicleColor);
            p.Add("@vehicle_year", subcontractProfileAsstEngineer.VehicleYear);
            p.Add("@vehicle_license_plate", subcontractProfileAsstEngineer.VehicleLicensePlate);
            p.Add("@vehicle_attach_file", subcontractProfileAsstEngineer.VehicleAttachFile);
            p.Add("@tool_otrd", subcontractProfileAsstEngineer.ToolOtrd);
            p.Add("@tool_splicing", subcontractProfileAsstEngineer.ToolSplicing);
            p.Add("@position", subcontractProfileAsstEngineer.Position);
            p.Add("@account_no", subcontractProfileAsstEngineer.AccountNo);
            p.Add("@account_name", subcontractProfileAsstEngineer.AccountName);
            p.Add("@personal_attach_file", subcontractProfileAsstEngineer.PersonalAttachFile);
            p.Add("@staff_status", subcontractProfileAsstEngineer.StaffStatus);
            p.Add("@create_date", subcontractProfileAsstEngineer.CreateDate);
            p.Add("@create_by", subcontractProfileAsstEngineer.CreateBy);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAsstEngineer_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer subcontractProfileAsstEngineer)
        {
            var p = new DynamicParameters();
            p.Add("@asst_engineer_id", subcontractProfileAsstEngineer.AsstEngineerId);
            p.Add("@staff_code", subcontractProfileAsstEngineer.StaffCode);
            p.Add("@staff_name", subcontractProfileAsstEngineer.StaffName);
            p.Add("@asc_code", subcontractProfileAsstEngineer.AscCode);
            p.Add("@tshirt_size", subcontractProfileAsstEngineer.TshirtSize);
            p.Add("@contract_phone1", subcontractProfileAsstEngineer.ContractPhone1);
            p.Add("@contract_phone2", subcontractProfileAsstEngineer.ContractPhone2);
            p.Add("@contract_email", subcontractProfileAsstEngineer.ContractEmail);
            p.Add("@work_experience", subcontractProfileAsstEngineer.WorkExperience);
            p.Add("@work_experience_attach_file", subcontractProfileAsstEngineer.WorkExperienceAttachFile);
            p.Add("@work_type", subcontractProfileAsstEngineer.WorkType);
            p.Add("@course_skill", subcontractProfileAsstEngineer.CourseSkill);
            p.Add("@skill_level", subcontractProfileAsstEngineer.SkillLevel);
            p.Add("@vehicle_type", subcontractProfileAsstEngineer.VehicleType);
            p.Add("@vehicle_brand", subcontractProfileAsstEngineer.VehicleBrand);
            p.Add("@vehicle_serise", subcontractProfileAsstEngineer.VehicleSerise);
            p.Add("@vehicle_color", subcontractProfileAsstEngineer.VehicleColor);
            p.Add("@vehicle_year", subcontractProfileAsstEngineer.VehicleYear);
            p.Add("@vehicle_license_plate", subcontractProfileAsstEngineer.VehicleLicensePlate);
            p.Add("@vehicle_attach_file", subcontractProfileAsstEngineer.VehicleAttachFile);
            p.Add("@tool_otrd", subcontractProfileAsstEngineer.ToolOtrd);
            p.Add("@tool_splicing", subcontractProfileAsstEngineer.ToolSplicing);
            p.Add("@position", subcontractProfileAsstEngineer.Position);
            p.Add("@account_no", subcontractProfileAsstEngineer.AccountNo);
            p.Add("@account_name", subcontractProfileAsstEngineer.AccountName);
            p.Add("@personal_attach_file", subcontractProfileAsstEngineer.PersonalAttachFile);
            p.Add("@staff_status", subcontractProfileAsstEngineer.StaffStatus);
            p.Add("@create_date", subcontractProfileAsstEngineer.CreateDate);
            p.Add("@create_by", subcontractProfileAsstEngineer.CreateBy);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAsstEngineer_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string asstEngineerId)
        {
            var p = new DynamicParameters();
            p.Add("@asst_engineer_id", asstEngineerId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAsstEngineer_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer> subcontractProfileAsstEngineerList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileAsstEngineerDataTable(subcontractProfileAsstEngineerList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileAsstEngineer_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileAsstEngineerDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer> SubcontractProfileAsstEngineerList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("asst_engineer_id", typeof(SqlGuid));
            dt.Columns.Add("staff_code", typeof(SqlString));
            dt.Columns.Add("staff_name", typeof(SqlString));
            dt.Columns.Add("asc_code", typeof(SqlString));
            dt.Columns.Add("tshirt_size", typeof(SqlString));
            dt.Columns.Add("contract_phone1", typeof(SqlString));
            dt.Columns.Add("contract_phone2", typeof(SqlString));
            dt.Columns.Add("contract_email", typeof(SqlString));
            dt.Columns.Add("work_experience", typeof(SqlString));
            dt.Columns.Add("work_experience_attach_file", typeof(SqlString));
            dt.Columns.Add("work_type", typeof(SqlString));
            dt.Columns.Add("course_skill", typeof(SqlString));
            dt.Columns.Add("skill_level", typeof(SqlString));
            dt.Columns.Add("vehicle_type", typeof(SqlString));
            dt.Columns.Add("vehicle_brand", typeof(SqlString));
            dt.Columns.Add("vehicle_serise", typeof(SqlString));
            dt.Columns.Add("vehicle_color", typeof(SqlString));
            dt.Columns.Add("vehicle_year", typeof(SqlString));
            dt.Columns.Add("vehicle_license_plate", typeof(SqlString));
            dt.Columns.Add("vehicle_attach_file", typeof(SqlString));
            dt.Columns.Add("tool_otrd", typeof(SqlString));
            dt.Columns.Add("tool_splicing", typeof(SqlString));
            dt.Columns.Add("position", typeof(SqlString));
            dt.Columns.Add("account_no", typeof(SqlString));
            dt.Columns.Add("account_name", typeof(SqlString));
            dt.Columns.Add("personal_attach_file", typeof(SqlString));
            dt.Columns.Add("staff_status", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("create_by", typeof(SqlString));

            if (SubcontractProfileAsstEngineerList != null)
                foreach (var curObj in SubcontractProfileAsstEngineerList)
                {
                    DataRow row = dt.NewRow();
                    row["asst_engineer_id"] = new SqlGuid(curObj.AsstEngineerId);
                    row["staff_code"] = new SqlString(curObj.StaffCode);
                    row["staff_name"] = new SqlString(curObj.StaffName);
                    row["asc_code"] = new SqlString(curObj.AscCode);
                    row["tshirt_size"] = new SqlString(curObj.TshirtSize);
                    row["contract_phone1"] = new SqlString(curObj.ContractPhone1);
                    row["contract_phone2"] = new SqlString(curObj.ContractPhone2);
                    row["contract_email"] = new SqlString(curObj.ContractEmail);
                    row["work_experience"] = new SqlString(curObj.WorkExperience);
                    row["work_experience_attach_file"] = new SqlString(curObj.WorkExperienceAttachFile);
                    row["work_type"] = new SqlString(curObj.WorkType);
                    row["course_skill"] = new SqlString(curObj.CourseSkill);
                    row["skill_level"] = new SqlString(curObj.SkillLevel);
                    row["vehicle_type"] = new SqlString(curObj.VehicleType);
                    row["vehicle_brand"] = new SqlString(curObj.VehicleBrand);
                    row["vehicle_serise"] = new SqlString(curObj.VehicleSerise);
                    row["vehicle_color"] = new SqlString(curObj.VehicleColor);
                    row["vehicle_year"] = new SqlString(curObj.VehicleYear);
                    row["vehicle_license_plate"] = new SqlString(curObj.VehicleLicensePlate);
                    row["vehicle_attach_file"] = new SqlString(curObj.VehicleAttachFile);
                    row["tool_otrd"] = new SqlString(curObj.ToolOtrd);
                    row["tool_splicing"] = new SqlString(curObj.ToolSplicing);
                    row["position"] = new SqlString(curObj.Position);
                    row["account_no"] = new SqlString(curObj.AccountNo);
                    row["account_name"] = new SqlString(curObj.AccountName);
                    row["personal_attach_file"] = new SqlString(curObj.PersonalAttachFile);
                    row["staff_status"] = new SqlString(curObj.StaffStatus);
                    row["create_date"] = new SqlDateTime(curObj.CreateDate);
                    row["create_by"] = new SqlString(curObj.CreateBy);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileAsstEngineerPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer>
                ("uspSubcontractProfileAsstEngineer_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileAsstEngineerPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileAsstEngineer_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("asst_engineer_id", typeof(SqlGuid));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["asst_engineer_id"] = new SqlGuid(curObj.AsstEngineerId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

       
    }

}
