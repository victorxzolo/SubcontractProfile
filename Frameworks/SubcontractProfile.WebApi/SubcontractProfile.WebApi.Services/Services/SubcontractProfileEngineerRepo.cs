﻿using Dapper;
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
    /// Description:	Class for the repo SubcontractProfileEngineerRepo 
    /// =================================================================
    public partial class SubcontractProfileEngineerRepo : ISubcontractProfileEngineerRepo
    {

        protected IDbContext _dbContext;

        public SubcontractProfileEngineerRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>
            ("uspSubcontractProfileEngineer_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer> GetByEngineerId(System.Guid engineerId)
        {
            var p = new DynamicParameters();
            p.Add("@engineer_id", engineerId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>
            ("uspSubcontractProfileEngineer_selectByEngineerId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }




        public async Task<bool> MigrationInsert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer subcontractProfileEngineer)
        {
            var p = new DynamicParameters();

            //  p.Add("@location_id", subcontractProfileLocation.LocationId);
            p.Add("@staff_code", subcontractProfileEngineer.StaffCode);
            p.Add("@foa_code", subcontractProfileEngineer.FoaCode);
            p.Add("@staff_name", subcontractProfileEngineer.StaffName);
            p.Add("@staff_name_th", subcontractProfileEngineer.StaffNameTh);
            p.Add("@staff_name_en", subcontractProfileEngineer.StaffNameEn);
            p.Add("@asc_code", subcontractProfileEngineer.AscCode);
            p.Add("@tshirt_size", subcontractProfileEngineer.TshirtSize);
            p.Add("@contract_phone1", subcontractProfileEngineer.ContractPhone1);
            p.Add("@contract_phone2", subcontractProfileEngineer.ContractPhone2);
            p.Add("@contract_email", subcontractProfileEngineer.ContractEmail);
            p.Add("@work_experience", subcontractProfileEngineer.WorkExperience);
            p.Add("@work_experience_attach_file", subcontractProfileEngineer.WorkExperienceAttachFile);
            p.Add("@work_type", subcontractProfileEngineer.WorkType);
            p.Add("@course_skill", subcontractProfileEngineer.CourseSkill);
            p.Add("@skill_level", subcontractProfileEngineer.SkillLevel);
            p.Add("@vehicle_type", subcontractProfileEngineer.VehicleType);
            p.Add("@vehicle_brand", subcontractProfileEngineer.VehicleBrand);
            p.Add("@vehicle_serise", subcontractProfileEngineer.VehicleSerise);
            p.Add("@vehicle_color", subcontractProfileEngineer.VehicleColor);
            p.Add("@vehicle_year", subcontractProfileEngineer.VehicleYear);
            p.Add("@vehicle_license_plate", subcontractProfileEngineer.VehicleLicensePlate);
            p.Add("@vehicle_attach_file", subcontractProfileEngineer.VehicleAttachFile);
            p.Add("@tool_otrd", subcontractProfileEngineer.ToolOtrd);
            p.Add("@tool_splicing", subcontractProfileEngineer.ToolSplicing);
            p.Add("@position", subcontractProfileEngineer.Position);
            p.Add("@location_code", subcontractProfileEngineer.LocationCode);
            p.Add("@staff_id", subcontractProfileEngineer.StaffId);
            p.Add("@team_code", subcontractProfileEngineer.TeamCode);
            p.Add("@citizen_id", subcontractProfileEngineer.CitizenId);
            p.Add("@bank_code", subcontractProfileEngineer.BankCode);
            p.Add("@bank_name", subcontractProfileEngineer.BankName);
            p.Add("@account_no", subcontractProfileEngineer.AccountNo);
            p.Add("@account_name", subcontractProfileEngineer.AccountName);
            p.Add("@personal_attach_file", subcontractProfileEngineer.PersonalAttachFile);
            p.Add("@staff_status", subcontractProfileEngineer.StaffStatus);
         

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileengineer_migrationinsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer subcontractProfileEngineer)
        {
            var p = new DynamicParameters();
            p.Add("@engineer_id", subcontractProfileEngineer.EngineerId);
            p.Add("@staff_code", subcontractProfileEngineer.StaffCode);
            p.Add("@foa_code", subcontractProfileEngineer.FoaCode);
            p.Add("@staff_name", subcontractProfileEngineer.StaffName);
            p.Add("@staff_name_th", subcontractProfileEngineer.StaffNameTh);
            p.Add("@staff_name_en", subcontractProfileEngineer.StaffNameEn);
            p.Add("@asc_code", subcontractProfileEngineer.AscCode);
            p.Add("@tshirt_size", subcontractProfileEngineer.TshirtSize);
            p.Add("@contract_phone1", subcontractProfileEngineer.ContractPhone1);
            p.Add("@contract_phone2", subcontractProfileEngineer.ContractPhone2);
            p.Add("@contract_email", subcontractProfileEngineer.ContractEmail);
            p.Add("@work_experience", subcontractProfileEngineer.WorkExperience);
            p.Add("@work_experience_attach_file", subcontractProfileEngineer.WorkExperienceAttachFile);
            p.Add("@work_type", subcontractProfileEngineer.WorkType);
            p.Add("@course_skill", subcontractProfileEngineer.CourseSkill);
            p.Add("@skill_level", subcontractProfileEngineer.SkillLevel);
            p.Add("@vehicle_type", subcontractProfileEngineer.VehicleType);
            p.Add("@vehicle_brand", subcontractProfileEngineer.VehicleBrand);
            p.Add("@vehicle_serise", subcontractProfileEngineer.VehicleSerise);
            p.Add("@vehicle_color", subcontractProfileEngineer.VehicleColor);
            p.Add("@vehicle_year", subcontractProfileEngineer.VehicleYear);
            p.Add("@vehicle_license_plate", subcontractProfileEngineer.VehicleLicensePlate);
            p.Add("@vehicle_attach_file", subcontractProfileEngineer.VehicleAttachFile);
            p.Add("@tool_otrd", subcontractProfileEngineer.ToolOtrd);
            p.Add("@tool_splicing", subcontractProfileEngineer.ToolSplicing);
            p.Add("@position", subcontractProfileEngineer.Position);
            p.Add("@location_code", subcontractProfileEngineer.LocationCode);
            p.Add("@staff_id", subcontractProfileEngineer.StaffId);
            p.Add("@team_code", subcontractProfileEngineer.TeamCode);
            p.Add("@citizen_id", subcontractProfileEngineer.CitizenId);
            p.Add("@bank_code", subcontractProfileEngineer.BankCode);
            p.Add("@bank_name", subcontractProfileEngineer.BankName);
            p.Add("@account_no", subcontractProfileEngineer.AccountNo);
            p.Add("@account_name", subcontractProfileEngineer.AccountName);
            p.Add("@personal_attach_file", subcontractProfileEngineer.PersonalAttachFile);
            p.Add("@staff_status", subcontractProfileEngineer.StaffStatus);
            p.Add("@create_by", subcontractProfileEngineer.CreateBy);
            p.Add("@company_id", subcontractProfileEngineer.CompanyId);
            p.Add("@location_id", subcontractProfileEngineer.LocationId);
            p.Add("@team_id", subcontractProfileEngineer.TeamId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileEngineer_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer subcontractProfileEngineer)
        {
            var p = new DynamicParameters();
            p.Add("@engineer_id", subcontractProfileEngineer.EngineerId);
            p.Add("@staff_code", subcontractProfileEngineer.StaffCode);
            p.Add("@foa_code", subcontractProfileEngineer.FoaCode);
            p.Add("@staff_name", subcontractProfileEngineer.StaffName);
            p.Add("@staff_name_th", subcontractProfileEngineer.StaffNameTh);
            p.Add("@staff_name_en", subcontractProfileEngineer.StaffNameEn);
            p.Add("@asc_code", subcontractProfileEngineer.AscCode);
            p.Add("@tshirt_size", subcontractProfileEngineer.TshirtSize);
            p.Add("@contract_phone1", subcontractProfileEngineer.ContractPhone1);
            p.Add("@contract_phone2", subcontractProfileEngineer.ContractPhone2);
            p.Add("@contract_email", subcontractProfileEngineer.ContractEmail);
            p.Add("@work_experience", subcontractProfileEngineer.WorkExperience);
            p.Add("@work_experience_attach_file", subcontractProfileEngineer.WorkExperienceAttachFile);
            p.Add("@work_type", subcontractProfileEngineer.WorkType);
            p.Add("@course_skill", subcontractProfileEngineer.CourseSkill);
            p.Add("@skill_level", subcontractProfileEngineer.SkillLevel);
            p.Add("@vehicle_type", subcontractProfileEngineer.VehicleType);
            p.Add("@vehicle_brand", subcontractProfileEngineer.VehicleBrand);
            p.Add("@vehicle_serise", subcontractProfileEngineer.VehicleSerise);
            p.Add("@vehicle_color", subcontractProfileEngineer.VehicleColor);
            p.Add("@vehicle_year", subcontractProfileEngineer.VehicleYear);
            p.Add("@vehicle_license_plate", subcontractProfileEngineer.VehicleLicensePlate);
            p.Add("@vehicle_attach_file", subcontractProfileEngineer.VehicleAttachFile);
            p.Add("@tool_otrd", subcontractProfileEngineer.ToolOtrd);
            p.Add("@tool_splicing", subcontractProfileEngineer.ToolSplicing);
            p.Add("@position", subcontractProfileEngineer.Position);
            p.Add("@location_code", subcontractProfileEngineer.LocationCode);
            p.Add("@staff_id", subcontractProfileEngineer.StaffId);
            p.Add("@team_code", subcontractProfileEngineer.TeamCode);
            p.Add("@citizen_id", subcontractProfileEngineer.CitizenId);
            p.Add("@bank_code", subcontractProfileEngineer.BankCode);
            p.Add("@bank_name", subcontractProfileEngineer.BankName);
            p.Add("@account_no", subcontractProfileEngineer.AccountNo);
            p.Add("@account_name", subcontractProfileEngineer.AccountName);
            p.Add("@personal_attach_file", subcontractProfileEngineer.PersonalAttachFile);
            p.Add("@staff_status", subcontractProfileEngineer.StaffStatus);
            p.Add("@update_by", subcontractProfileEngineer.UpdateBy);
            p.Add("@company_id", subcontractProfileEngineer.CompanyId);
            p.Add("@location_id", subcontractProfileEngineer.LocationId);
            p.Add("@team_id", subcontractProfileEngineer.TeamId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileEngineer_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string engineerId)
        {
            var p = new DynamicParameters();
            p.Add("@engineer_id", engineerId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileEngineer_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer> subcontractProfileEngineerList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileEngineerDataTable(subcontractProfileEngineerList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileEngineer_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileEngineerDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer> SubcontractProfileEngineerList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("engineer_id", typeof(SqlGuid));
            dt.Columns.Add("staff_code", typeof(SqlString));
            dt.Columns.Add("foa_code", typeof(SqlString));
            dt.Columns.Add("staff_name", typeof(SqlString));
            dt.Columns.Add("staff_name_th", typeof(SqlString));
            dt.Columns.Add("staff_name_en", typeof(SqlString));
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
            dt.Columns.Add("location_code", typeof(SqlString));
            dt.Columns.Add("staff_id", typeof(SqlString));
            dt.Columns.Add("team_code", typeof(SqlString));
            dt.Columns.Add("citizen_id", typeof(SqlString));
            dt.Columns.Add("bank_code", typeof(SqlString));
            dt.Columns.Add("bank_name", typeof(SqlString));
            dt.Columns.Add("account_no", typeof(SqlString));
            dt.Columns.Add("account_name", typeof(SqlString));
            dt.Columns.Add("personal_attach_file", typeof(SqlString));
            dt.Columns.Add("staff_status", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("create_by", typeof(SqlString));
            dt.Columns.Add("update_by", typeof(SqlString));
            dt.Columns.Add("update_date", typeof(SqlDateTime));

            if (SubcontractProfileEngineerList != null)
                foreach (var curObj in SubcontractProfileEngineerList)
                {
                    DataRow row = dt.NewRow();
                    row["engineer_id"] = new SqlGuid(curObj.EngineerId);
                    row["staff_code"] = new SqlString(curObj.StaffCode);
                    row["foa_code"] = new SqlString(curObj.FoaCode);
                    row["staff_name"] = new SqlString(curObj.StaffName);
                    row["staff_name_th"] = new SqlString(curObj.StaffNameTh);
                    row["staff_name_en"] = new SqlString(curObj.StaffNameEn);
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
                    row["location_code"] = new SqlString(curObj.LocationCode);
                    row["staff_id"] = new SqlString(curObj.StaffId);
                    row["team_code"] = new SqlString(curObj.TeamCode);
                    row["citizen_id"] = new SqlString(curObj.CitizenId);
                    row["bank_code"] = new SqlString(curObj.BankCode);
                    row["bank_name"] = new SqlString(curObj.BankName);
                    row["account_no"] = new SqlString(curObj.AccountNo);
                    row["account_name"] = new SqlString(curObj.AccountName);
                    row["personal_attach_file"] = new SqlString(curObj.PersonalAttachFile);
                    row["staff_status"] = new SqlString(curObj.StaffStatus);
                    row["create_date"] = curObj.CreateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.CreateDate.Value);
                    row["create_by"] = new SqlString(curObj.CreateBy);
                    row["update_by"] = new SqlString(curObj.UpdateBy);
                    row["update_date"] = curObj.UpdateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.UpdateDate.Value);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileEngineerPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>
                ("uspSubcontractProfileEngineer_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileEngineerPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("engineer_id", typeof(SqlGuid));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["engineer_id"] = new SqlGuid(curObj.EngineerId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        public async Task<IEnumerable<SubcontractProfileEngineer>> SearchEngineer(Guid companyId, Guid locationId,
            Guid teamId, string staffName, string citizenId, string position)
        {
            var p = new DynamicParameters();
            p.Add("@company_id", companyId);
            p.Add("@location_id", locationId);
            p.Add("@team_id", teamId);
            p.Add("@staff_name ", staffName);
            p.Add("@citizen_id", citizenId);
            p.Add("@position", position);
       
            var entity = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>
            ("uspSubcontractProfileEngineer_searchEngineer", p, commandType: CommandType.StoredProcedure);

            return entity;
        }



        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> selectEngineer(
       string citizen_id, string staff_name, string contact_phone, string date_from, string date_to)
        {
            var p = new DynamicParameters();

            p.Add("@citizen_id", citizen_id);
            p.Add("@staff_name", staff_name);
            p.Add("@contact_phone", contact_phone);
            p.Add("@date_from", date_from);
            p.Add("@date_to", date_to);


            var entity = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>
            ("uspSubcontractProfileEngineer_selectEngineer", p, commandType: CommandType.StoredProcedure);

            return entity;
        }




        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>> selectEngineerAll(string citizen_id, string staff_name, string contract_phone, string date_from, string date_to)
        {
            var p = new DynamicParameters();
            p.Add("@citizen_id", citizen_id);
            p.Add("@staff_name", staff_name);
            p.Add("@contract_phone", contract_phone);
            p.Add("@date_from ", date_from);
            p.Add("@date_to", date_to);
           

            var entity = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>
            ("uspSubcontractProfileEngineer_selectEngineerAll", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        public async Task<IEnumerable<SubcontractProfileEngineer>> GetEngineerByTeam(Guid companyId, Guid locationId, Guid teamId)
        {
            var p = new DynamicParameters();
            p.Add("@company_id", companyId);
            p.Add("@location_id", locationId);
            p.Add("@team_id", teamId);
          
            var entity = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>
            ("uspSubcontractProfileEngineer_selectByTeam", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        public async Task<IEnumerable<SubcontractProfileEngineer>> GetEngineerByCompany(Guid companyId)
        {
            var p = new DynamicParameters();
            p.Add("@company_id", companyId);

            var entity = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineer>
            ("uspSubcontractProfileEngineer_selectByCompany", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        public async Task<IEnumerable<SubcontractProfileEngineerBlacklist>> CheckBlacklist(string id_card)
        {
            var p = new DynamicParameters();
            p.Add("@id_card", id_card);

            var entity = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileEngineerBlacklist>
            ("uspSubcontractProfileEngineer_CheckBlacklist", p, commandType: CommandType.StoredProcedure);

            return entity;
        }
    }
}
