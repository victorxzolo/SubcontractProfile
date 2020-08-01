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
    /// Description:	Class for the repo SubcontractProfilePaymentRepo 
    /// =================================================================
    public partial class SubcontractProfilePaymentRepo : ISubcontractProfilePaymentRepo
    {

        protected IDbContext _dbContext;

        public SubcontractProfilePaymentRepo(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment>
            ("uspSubcontractProfilePayment_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment> GetByPaymentId(string paymentId)
        {
            var p = new DynamicParameters();
            p.Add("@payment_id", paymentId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment>
            ("uspSubcontractProfilePayment_selectByPaymentId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment subcontractProfilePayment)
        {
            var p = new DynamicParameters();

            p.Add("@payment_id", subcontractProfilePayment.PaymentId);
            p.Add("@payment_no", subcontractProfilePayment.PaymentNo);
            p.Add("@payment_channal", subcontractProfilePayment.PaymentChannal);
            p.Add("@payment_datetime", subcontractProfilePayment.PaymentDatetime);
            p.Add("@amount_transfer", subcontractProfilePayment.AmountTransfer);
            p.Add("@bank_transfer", subcontractProfilePayment.BankTransfer);
            p.Add("@bank_branch", subcontractProfilePayment.BankBranch);
            p.Add("@slip_attach_file", subcontractProfilePayment.SlipAttachFile);
            p.Add("@contact_name", subcontractProfilePayment.ContactName);
            p.Add("@contact_phone_no", subcontractProfilePayment.ContactPhoneNo);
            p.Add("@contact_email", subcontractProfilePayment.ContactEmail);
            p.Add("@remark", subcontractProfilePayment.Remark);
            p.Add("@status", subcontractProfilePayment.Status);
            p.Add("@create_date", subcontractProfilePayment.CreateDate);
            p.Add("@create_by", subcontractProfilePayment.CreateBy);
            p.Add("@modified_by", subcontractProfilePayment.ModifiedBy);
            p.Add("@modified_date", subcontractProfilePayment.ModifiedDate);
            p.Add("@training_id", subcontractProfilePayment.TrainingId);
            p.Add("@company_id", subcontractProfilePayment.CompanyId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfilePayment_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment subcontractProfilePayment)
        {
            var p = new DynamicParameters();
            p.Add("@payment_id", subcontractProfilePayment.PaymentId);
            p.Add("@payment_no", subcontractProfilePayment.PaymentNo);
            p.Add("@payment_channal", subcontractProfilePayment.PaymentChannal);
            p.Add("@payment_datetime", subcontractProfilePayment.PaymentDatetime);
            p.Add("@amount_transfer", subcontractProfilePayment.AmountTransfer);
            p.Add("@bank_transfer", subcontractProfilePayment.BankTransfer);
            p.Add("@bank_branch", subcontractProfilePayment.BankBranch);
            p.Add("@slip_attach_file", subcontractProfilePayment.SlipAttachFile);
            p.Add("@contact_name", subcontractProfilePayment.ContactName);
            p.Add("@contact_phone_no", subcontractProfilePayment.ContactPhoneNo);
            p.Add("@contact_email", subcontractProfilePayment.ContactEmail);
            p.Add("@remark", subcontractProfilePayment.Remark);
            p.Add("@status", subcontractProfilePayment.Status);
            p.Add("@create_date", subcontractProfilePayment.CreateDate);
            p.Add("@create_by", subcontractProfilePayment.CreateBy);
            p.Add("@modified_by", subcontractProfilePayment.ModifiedBy);
            p.Add("@modified_date", subcontractProfilePayment.ModifiedDate);
            p.Add("@training_id", subcontractProfilePayment.TrainingId);
            p.Add("@company_id", subcontractProfilePayment.CompanyId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfilePayment_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string paymentId)
        {
            var p = new DynamicParameters();
            p.Add("@payment_id", paymentId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfilePayment_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment> subcontractProfilePaymentList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfilePaymentDataTable(subcontractProfilePaymentList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfilePayment_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfilePaymentDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment> SubcontractProfilePaymentList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("payment_id", typeof(SqlString));
            dt.Columns.Add("payment_no", typeof(SqlString));
            dt.Columns.Add("payment_channal", typeof(SqlString));
            dt.Columns.Add("payment_datetime", typeof(SqlDateTime));
            dt.Columns.Add("amount_transfer", typeof(SqlDecimal));
            dt.Columns.Add("bank_transfer", typeof(SqlString));
            dt.Columns.Add("bank_branch", typeof(SqlString));
            dt.Columns.Add("slip_attach_file", typeof(SqlString));
            dt.Columns.Add("contact_name", typeof(SqlString));
            dt.Columns.Add("contact_phone_no", typeof(SqlString));
            dt.Columns.Add("contact_email", typeof(SqlString));
            dt.Columns.Add("remark", typeof(SqlString));
            dt.Columns.Add("status", typeof(SqlString));
            dt.Columns.Add("create_date", typeof(SqlDateTime));
            dt.Columns.Add("create_by", typeof(SqlString));
            dt.Columns.Add("modified_by", typeof(SqlString));
            dt.Columns.Add("modified_date", typeof(SqlDateTime));
            dt.Columns.Add("training_id", typeof(SqlString));
            dt.Columns.Add("company_id", typeof(SqlString));

            if (SubcontractProfilePaymentList != null)
                foreach (var curObj in SubcontractProfilePaymentList)
                {
                    DataRow row = dt.NewRow();
                    row["payment_id"] = new SqlString(curObj.PaymentId);
                    row["payment_no"] = new SqlString(curObj.PaymentNo);
                    row["payment_channal"] = new SqlString(curObj.PaymentChannal);
                    row["payment_datetime"] = new SqlDateTime(curObj.PaymentDatetime);
                    row["amount_transfer"] = new SqlDecimal(curObj.AmountTransfer);
                    row["bank_transfer"] = new SqlString(curObj.BankTransfer);
                    row["bank_branch"] = new SqlString(curObj.BankBranch);
                    row["slip_attach_file"] = new SqlString(curObj.SlipAttachFile);
                    row["contact_name"] = new SqlString(curObj.ContactName);
                    row["contact_phone_no"] = new SqlString(curObj.ContactPhoneNo);
                    row["contact_email"] = new SqlString(curObj.ContactEmail);
                    row["remark"] = new SqlString(curObj.Remark);
                    row["status"] = new SqlString(curObj.Status);
                    row["create_date"] = curObj.CreateDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.CreateDate.Value);
                    row["create_by"] = new SqlString(curObj.CreateBy);
                    row["modified_by"] = new SqlString(curObj.ModifiedBy);
                    row["modified_date"] = curObj.ModifiedDate == null ? SqlDateTime.Null : new SqlDateTime(curObj.ModifiedDate.Value);
                    row["training_id"] = new SqlString(curObj.TrainingId);
                    row["company_id"] = new SqlString(curObj.CompanyId);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfilePaymentPKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment>
                ("uspSubcontractProfilePayment_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfilePaymentPKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("payment_id", typeof(SqlString));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["payment_id"] = new SqlString(curObj.PaymentId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        public async Task<SubcontractProfilePayment> searchPayment(string payment_no, string request_training_no,
            string request_date_from, string request_date_to, string payment_date_from, string payment_date_to,
            string payment_status)
        {
            var p = new DynamicParameters();
            p.Add("@payment_no", payment_no);
            p.Add("@request_training_no", request_training_no);
            p.Add("@request_date_from", request_date_from);
            p.Add("@request_date_to", request_date_to);
            p.Add("@payment_date_from", payment_date_from);
            p.Add("@payment_date_to", payment_date_to);
            p.Add("@payment_status", payment_status);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfilePayment>
            ("uspSubcontractProfilePayment_searchPayment", p, commandType: CommandType.StoredProcedure);

            return entity;
        }
    }
}
