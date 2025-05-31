using POS_API.BusinessObjects;
using POS_API.Models;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;
using Dapper;
using System.Data;

namespace POS_API.Services
{
    public class SQLService
    {
        private readonly IDbConnection _dbConnection;

        public SQLService(IDbConnection connectionString)
        {
            _dbConnection = connectionString;
        }

        public async Task<int> GetMaxId(string tableName, string columnName)
        {
            int nextId = 1;
            string query = $"SELECT ISNULL(MAX({columnName}), 0) + 1 AS max_id FROM {tableName}";

            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                var result = await _dbConnection.ExecuteScalarAsync<int>(query);
                nextId = result;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching next ID from {tableName}: {ex.Message}", ex);
            }

            return nextId;
        }

        public async Task<int> GetMaxId(string tableName, string columnName, string etype, DateTime? vrdate)
        {
            string query = $@"
        SELECT ISNULL(MAX({columnName}), 0) + 1 
        FROM {tableName} 
        WHERE dtVrDate = @VrDate AND varVrType = @VrType";

            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                return await _dbConnection.ExecuteScalarAsync<int>(query, new { VrDate = vrdate, VrType = etype });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting max ID for {tableName}: {ex.Message}", ex);
            }
        }

        public async Task<List<Level2>> GetLevel2()
        {
            string query = @"
        SELECT l2.*, l1.varLevel1Name 
        FROM tblLevel2 l2 
        INNER JOIN tblLevel1 l1 ON l2.intLevel1Id = l1.intLevel1Id";

            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                var result = await _dbConnection.QueryAsync<Level2>(query);
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Level2 data: {ex.Message}", ex);
            }
        }


        public async Task<Level2?> GetLevel2ById(int id)
        {
            string query = @"
            SELECT l2.*, l1.varLevel1Name 
            FROM tblLevel2 l2 
            INNER JOIN tblLevel1 l1 ON l2.intLevel1Id = l1.intLevel1Id 
            WHERE l2.intLevel2Id = @Id";

            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                return await _dbConnection.QueryFirstOrDefaultAsync<Level2?>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Level2 by ID: {ex.Message}", ex);
            }
        }


        public async Task<List<Level3>> GetLevel3()
        {
            string query = @"
        SELECT l3.*, l2.varLevel2Name 
        FROM tblLevel3 l3 
        INNER JOIN tblLevel2 l2 ON l3.intLevel2Id = l2.intLevel2Id";

            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                var result = await _dbConnection.QueryAsync<Level3>(query);
                return result.AsList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Level3 data: {ex.Message}", ex);
            }
        }


        public async Task<Level3?> GetLevel3ById(int id)
        {
            string query = @"
            SELECT l3.*, l2.varLevel2Name 
            FROM tblLevel3 l3 
            INNER JOIN tblLevel2 l2 ON l3.intLevel2Id = l2.intLevel2Id 
            WHERE l3.intLevel3Id = @Id";

            try
            {
                if (_dbConnection.State != ConnectionState.Open)
                    _dbConnection.Open();

                return await _dbConnection.QueryFirstOrDefaultAsync<Level3?>(query, new { Id = id });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error fetching Level3 by ID: {ex.Message}", ex);
            }
        }

    }
}
