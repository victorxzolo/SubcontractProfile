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
    /// Description:	Class for the repo SubcontractProfilePersonalRepo 
    /// =================================================================
    public partial class SubcontractProfilePersonalRepo : ISubcontractProfilePersonalRepo
    {

        protected IDbContext _dbContext;

        public SubcontractProfilePersonalRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal>
            ("uspSubcontractProfilePersonal_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal> GetByPersonalId(System.Guid personalId)
        {
            var p = new DynamicParameters();
            p.Add("@personal_id", personalId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal>
            ("uspSubcontractProfilePersonal_selectByPersonalId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal subcontractProfilePersonal)
        {
            var p = new DynamicParameters();

            p.Add("@personal_id", subcontractProfilePersonal.PersonalId);
            p.Add("@citizen_id", subcontractProfilePersonal.CitizenId);
            p.Add("@title_name", subcontractProfilePersonal.TitleName);
            p.Add("@full_name_en", subcontractProfilePersonal.FullNameEn);
            p.Add("@full_name_th", subcontractProfilePersonal.FullNameTh);
            p.Add("@birth_date", subcontractProfilePersonal.BirthDate);
            p.Add("@gender", subcontractProfilePersonal.Gender);
            p.Add("@race", subcontractProfilePersonal.Race);
            p.Add("@nationality", subcontractProfilePersonal.Nationality);
            p.Add("@religion", subcontractProfilePersonal.Religion);
            p.Add("@passport_attach_file", subcontractProfilePersonal.PassportAttachFile);
            p.Add("@identity_by", subcontractProfilePersonal.IdentityBy);
            p.Add("@address_id", subcontractProfilePersonal.AddressId);
            p.Add("@identity_card_address", subcontractProfilePersonal.IdentityCardAddress);
            p.Add("@contact_phone1", subcontractProfilePersonal.ContactPhone1);
            p.Add("@contact_phone2", subcontractProfilePersonal.ContactPhone2);
            p.Add("@contact_email", subcontractProfilePersonal.ContactEmail);
            p.Add("@work_permit_no", subcontractProfilePersonal.WorkPermitNo);
            p.Add("@work_permit_attach_file", subcontractProfilePersonal.WorkPermitAttachFile);
            p.Add("@profile_img_attach_file", subcontractProfilePersonal.ProfileImgAttachFile);
            p.Add("@education", subcontractProfilePersonal.Education);
            p.Add("@th_listening", subcontractProfilePersonal.ThListening);
            p.Add("@th_speaking", subcontractProfilePersonal.ThSpeaking);
            p.Add("@th_reading", subcontractProfilePersonal.ThReading);
            p.Add("@th_writing", subcontractProfilePersonal.ThWriting);
            p.Add("@en_listening", subcontractProfilePersonal.EnListening);
            p.Add("@en_speaking", subcontractProfilePersonal.EnSpeaking);
            p.Add("@en_reading", subcontractProfilePersonal.EnReading);
            p.Add("@en_writing", subcontractProfilePersonal.EnWriting);
            p.Add("@certificate_type", subcontractProfilePersonal.CertificateType);
            p.Add("@certificate_no", subcontractProfilePersonal.CertificateNo);
            p.Add("@certificate_expire_date", subcontractProfilePersonal.CertificateExpireDate);
            p.Add("@certificate_attach_file", subcontractProfilePersonal.CertificateAttachFile);
            p.Add("@bank_code", subcontractProfilePersonal.BankCode);
            p.Add("@bank_name", subcontractProfilePersonal.BankName);
            p.Add("@account_number", subcontractProfilePersonal.AccountNumber);
            p.Add("@account_name", subcontractProfilePersonal.AccountName);
            p.Add("@status", subcontractProfilePersonal.Status);
            p.Add("@create_date", subcontractProfilePersonal.CreateDate);
            p.Add("@create_by", subcontractProfilePersonal.CreateBy);
            p.Add("@update_by", subcontractProfilePersonal.UpdateBy);
            p.Add("@update_date", subcontractProfilePersonal.UpdateDate);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfilePersonal_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal subcontractProfilePersonal)
        {
            var p = new DynamicParameters();
            p.Add("@personal_id", subcontractProfilePersonal.PersonalId);
            p.Add("@citizen_id", subcontractProfilePersonal.CitizenId);
            p.Add("@title_name", subcontractProfilePersonal.TitleName);
            p.Add("@full_name_en", subcontractProfilePersonal.FullNameEn);
            p.Add("@full_name_th", subcontractProfilePersonal.FullNameTh);
            p.Add("@birth_date", subcontractProfilePersonal.BirthDate);
            p.Add("@gender", subcontractProfilePersonal.Gender);
            p.Add("@race", subcontractProfilePersonal.Race);
            p.Add("@nationality", subcontractProfilePersonal.Nationality);
            p.Add("@religion", subcontractProfilePersonal.Religion);
            p.Add("@passport_attach_file", subcontractProfilePersonal.PassportAttachFile);
            p.Add("@identity_by", subcontractProfilePersonal.IdentityBy);
            p.Add("@address_id", subcontractProfilePersonal.AddressId);
            p.Add("@identity_card_address", subcontractProfilePersonal.IdentityCardAddress);
            p.Add("@contact_phone1", subcontractProfilePersonal.ContactPhone1);
            p.Add("@contact_phone2", subcontractProfilePersonal.ContactPhone2);
            p.Add("@contact_email", subcontractProfilePersonal.ContactEmail);
            p.Add("@work_permit_no", subcontractProfilePersonal.WorkPermitNo);
            p.Add("@work_permit_attach_file", subcontractProfilePersonal.WorkPermitAttachFile);
            p.Add("@profile_img_attach_file", subcontractProfilePersonal.ProfileImgAttachFile);
            p.Add("@education", subcontractProfilePersonal.Education);
            p.Add("@th_listening", subcontractProfilePersonal.ThListening);
            p.Add("@th_speaking", subcontractProfilePersonal.ThSpeaking);
            p.Add("@th_reading", subcontractProfilePersonal.ThReading);
            p.Add("@th_writing", subcontractProfilePersonal.ThWriting);
            p.Add("@en_listening", subcontractProfilePersonal.EnListening);
            p.Add("@en_speaking", subcontractProfilePersonal.EnSpeaking);
            p.Add("@en_reading", subcontractProfilePersonal.EnReading);
            p.Add("@en_writing", subcontractProfilePersonal.EnWriting);
            p.Add("@certificate_type", subcontractProfilePersonal.CertificateType);
            p.Add("@certificate_no", subcontractProfilePersonal.CertificateNo);
            p.Add("@certificate_expire_date", subcontractProfilePersonal.CertificateExpireDate);
            p.Add("@certificate_attach_file", subcontractProfilePersonal.CertificateAttachFile);
            p.Add("@bank_code", subcontractProfilePersonal.BankCode);
            p.Add("@bank_name", subcontractProfilePersonal.BankName);
            p.Add("@account_number", subcontractProfilePersonal.AccountNumber);
            p.Add("@account_name", subcontractProfilePersonal.AccountName);
            p.Add("@status", subcontractProfilePersonal.Status);
            p.Add("@create_date", subcontractProfilePersonal.CreateDate);
            p.Add("@create_by", subcontractProfilePersonal.CreateBy);
            p.Add("@update_by", subcontractProfilePersonal.UpdateBy);
            p.Add("@update_date", subcontractProfilePersonal.UpdateDate);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfilePersonal_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string personalId)
        {
            var p = new DynamicParameters();
            p.Add("@personal_id", personalId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfilePersonal_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal> subcontractProfilePersonalList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfilePersonalDataTable(subcontractProfilePersonalList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfilePersonal_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfilePersonalDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal> SubcontractProfilePersonalList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("personal_id", typeof(SqlGuid));
            dt.Columns.Add("citizen_id", typeof(SqlString));
            dt.Columns.Add("title_name", typeof(SqlString));
            dt.Columns.Add("full_name_en", typeof(SqlString));
            dt.Columns.Add("full_name_th", typeof(SqlString));
            dt.Columns.Add("birth_date", typeof(SqlDateTime));
            dt.Columns.Add("gender", typeof(SqlString));
            dt.Columns.Add("race", typeof(SqlString));
            dt.Columns.Add("nationality", typeof(SqlString));
            dt.Columns.Add("religion", typeof(SqlString));
            dt.Columns.Add("passport_attach_file", typeof(SqlString));
            dt.Columns.Add("identity_by", typeof(SqlString));
            dt.Columns.Add("address_id", typeof(SqlInt32));
            dt.Columns.Add("identity_card_address", typeof(SqlString));
            dt.Columns.Add("contact_phone1", typeof(SqlString));
            dt.Columns.Add("contact_phone2", typeof(SqlString));
            dt.Columns.Add("contact_email", typeof(SqlString));
            dt.Columns.Add("work_permit_no", typeof(SqlString));
            dt.Columns.Add("work_permit_attach_file", typeof(SqlString));
            dt.Columns.Add("profile_img_attach_file", typeof(SqlString));
            dt.Columns.Add("education", typeof(SqlString));
            dt.Columns.Add("th_listening", typeof(SqlString));
            dt.Columns.Add("th_speaking", typeof(SqlString));
            dt.Columns.Add("th_reading", typeof(SqlString));
            dt.Columns.Add("th_writing", typeof(SqlString));
            dt.Columns.Add("en_listening", typeof(SqlString));
            dt.Columns.Add("en_speaking", typeof(SqlString));
            dt.Columns.Add("en_reading", typeof(SqlString));
            dt.Columns.Add("en_writing", typeof(SqlString));
            dt.Columns.Add("certificate_type", typeof(SqlString));
            dt.Columns.Add("certificate_no", typeof(SqlString));
            dt.Columns.Add("certificate_expire_date", typeof(SqlDateTime));
            dt.Columns.Add("certificate_attach_file", typeof(SqlString));
            dt.Columns.Add("bank_code", typeof(SqlString));
            dt.Columns.Add("bank_name", typeof(SqlString));
            dt.Columns.Add("account_number", typeof(SqlString));
            dt.Columns.Add("account_name", typeof(SqlString));
            dt.Columns.Add("status", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("create_by", typeof(SqlString));
            dt.Columns.Add("update_by", typeof(SqlString));
            dt.Columns.Add("update_date", typeof(SqlDateTime));

            if (SubcontractProfilePersonalList != null)
                foreach (var curObj in SubcontractProfilePersonalList)
                {
                    DataRow row = dt.NewRow();
                    row["personal_id"] = new SqlGuid(curObj.PersonalId);
                    row["citizen_id"] = new SqlString(curObj.CitizenId);
                    row["title_name"] = new SqlString(curObj.TitleName);
                    row["full_name_en"] = new SqlString(curObj.FullNameEn);
                    row["full_name_th"] = new SqlString(curObj.FullNameTh);
                    row["birth_date"] = curObj.BirthDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.BirthDate.Value);
                    row["gender"] = new SqlString(curObj.Gender);
                    row["race"] = new SqlString(curObj.Race);
                    row["nationality"] = new SqlString(curObj.Nationality);
                    row["religion"] = new SqlString(curObj.Religion);
                    row["passport_attach_file"] = new SqlString(curObj.PassportAttachFile);
                    row["identity_by"] = new SqlString(curObj.IdentityBy);
                    row["address_id"] = curObj.AddressId == null ? SqlInt32.Null : new SqlInt32((int)curObj.AddressId.Value);
                    row["identity_card_address"] = new SqlString(curObj.IdentityCardAddress);
                    row["contact_phone1"] = new SqlString(curObj.ContactPhone1);
                    row["contact_phone2"] = new SqlString(curObj.ContactPhone2);
                    row["contact_email"] = new SqlString(curObj.ContactEmail);
                    row["work_permit_no"] = new SqlString(curObj.WorkPermitNo);
                    row["work_permit_attach_file"] = new SqlString(curObj.WorkPermitAttachFile);
                    row["profile_img_attach_file"] = new SqlString(curObj.ProfileImgAttachFile);
                    row["education"] = new SqlString(curObj.Education);
                    row["th_listening"] = new SqlString(curObj.ThListening);
                    row["th_speaking"] = new SqlString(curObj.ThSpeaking);
                    row["th_reading"] = new SqlString(curObj.ThReading);
                    row["th_writing"] = new SqlString(curObj.ThWriting);
                    row["en_listening"] = new SqlString(curObj.EnListening);
                    row["en_speaking"] = new SqlString(curObj.EnSpeaking);
                    row["en_reading"] = new SqlString(curObj.EnReading);
                    row["en_writing"] = new SqlString(curObj.EnWriting);
                    row["certificate_type"] = new SqlString(curObj.CertificateType);
                    row["certificate_no"] = new SqlString(curObj.CertificateNo);
                    row["certificate_expire_date"] = curObj.CertificateExpireDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.CertificateExpireDate.Value);
                    row["certificate_attach_file"] = new SqlString(curObj.CertificateAttachFile);
                    row["bank_code"] = new SqlString(curObj.BankCode);
                    row["bank_name"] = new SqlString(curObj.BankName);
                    row["account_number"] = new SqlString(curObj.AccountNumber);
                    row["account_name"] = new SqlString(curObj.AccountName);
                    row["status"] = new SqlString(curObj.Status);
                    row["create_date"] = new SqlDateTime(curObj.CreateDate);
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
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfilePersonalPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal>
                ("uspSubcontractProfilePersonal_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfilePersonalPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePersonal_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("personal_id", typeof(SqlGuid));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["personal_id"] = new SqlGuid(curObj.PersonalId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }
}
