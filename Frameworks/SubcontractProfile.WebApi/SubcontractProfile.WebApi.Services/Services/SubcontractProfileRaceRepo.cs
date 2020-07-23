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
    /// Description:	Class for the repo SubcontractProfileRaceRepo 
    /// =================================================================
    public partial class SubcontractProfileRaceRepo : ISubcontractProfileRaceRepo
    {

        protected Repository.DbContext _dbContext = null;

        public SubcontractProfileRaceRepo(Repository.DbContext dbContext)
        {
            _dbContext = dbContext;
        }


        /// <summary>
        /// Get all
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace>> GetAll()
        {
            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace>
            ("uspSubcontractProfileRace_selectAll", commandType: CommandType.StoredProcedure);

            return entities;
        }

        /// <summary>
        /// Get by PK
        /// </summary>
        public async Task<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace> GetByRaceId(string raceId)
        {
            var p = new DynamicParameters();
            p.Add("@race_id", raceId);

            var entity = await _dbContext.Connection.QuerySingleOrDefaultAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace>
            ("uspSubcontractProfileRace_selectByRaceId", p, commandType: CommandType.StoredProcedure);

            return entity;
        }

        /// <summary>
        /// Insert
        /// </summary>
        public async Task<bool> Insert(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace subcontractProfileRace)
        {
            var p = new DynamicParameters();

            p.Add("@race_id", subcontractProfileRace.RaceId);
            p.Add("@race_name_th", subcontractProfileRace.RaceNameTh);
            p.Add("@race_name_en", subcontractProfileRace.RaceNameEn);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileRace_Insert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Update
        /// </summary>
        public async Task<bool> Update(SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace subcontractProfileRace)
        {
            var p = new DynamicParameters();
            p.Add("@race_id", subcontractProfileRace.RaceId);
            p.Add("@race_name_th", subcontractProfileRace.RaceNameTh);
            p.Add("@race_name_en", subcontractProfileRace.RaceNameEn);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileRace_Update", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Delete
        /// </summary>
        public async Task<bool> Delete(string raceId)
        {
            var p = new DynamicParameters();
            p.Add("@race_id", raceId);

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileRace_Delete", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Bulk insert
        /// </summary>
        public async Task<bool> BulkInsert(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace> subcontractProfileRaceList)
        {
            var p = new DynamicParameters();
            p.Add("@items", CreateSubcontractProfileRaceDataTable(subcontractProfileRaceList));

            var ok = await _dbContext.Connection.ExecuteAsync
                ("uspSubcontractProfileRace_bulkInsert", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return true;
        }

        /// <summary>
        /// Create special db table for bulk insert
        /// </summary>
        private object CreateSubcontractProfileRaceDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace> SubcontractProfileRaceList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("race_id", typeof(SqlString));
            dt.Columns.Add("race_name_th", typeof(SqlString));
            dt.Columns.Add("race_name_en", typeof(SqlString));

            if (SubcontractProfileRaceList != null)
                foreach (var curObj in SubcontractProfileRaceList)
                {
                    DataRow row = dt.NewRow();
                    row["race_id"] = new SqlString(curObj.RaceId);
                    row["race_name_th"] = new SqlString(curObj.RaceNameTh);
                    row["race_name_en"] = new SqlString(curObj.RaceNameEn);

                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

        /// <summary>
        /// Select by PK List
        /// </summary>
        public async Task<IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace>> GetByPKList(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace_PK> pkList)
        {
            var p = new DynamicParameters();
            p.Add("@pk_list", CreateSubcontractProfileRacePKDataTable(pkList));

            var entities = await _dbContext.Connection.QueryAsync<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace>
                ("uspSubcontractProfileRace_selectByPKList", p, commandType: CommandType.StoredProcedure, transaction: _dbContext.Transaction);

            return entities;
        }

        /// <summary>
        /// Create special db table for select by PK List
        /// </summary>
        private object CreateSubcontractProfileRacePKDataTable(IEnumerable<SubcontractProfile.WebApi.Services.Model.SubcontractProfileRace_PK> pkList)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("race_id", typeof(SqlString));

            if (pkList != null)
                foreach (var curObj in pkList)
                {
                    DataRow row = dt.NewRow();
                    row["race_id"] = new SqlString(curObj.RaceId);
                    dt.Rows.Add(row);
                }

            return dt.AsTableValuedParameter();

        }

    }

}
