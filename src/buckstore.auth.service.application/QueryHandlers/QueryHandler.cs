using System;
using Npgsql;
using System.Data;

namespace buckstore.auth.service.application.QueryHandlers
{
    public class QueryHandler
    {
        private readonly string _connectionString = Environment.GetEnvironmentVariable("ConnectionString");

        internal IDbConnection DbConnection { get { return new NpgsqlConnection(_connectionString); } }
    }
}