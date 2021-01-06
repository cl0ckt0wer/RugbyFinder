using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExceptionController : Controller
    {
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public ExceptionController(IConfiguration configuration)
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        public async Task PostExeceptionAsync(ExceptionArgs ex)
        {
            var sql = @"INSERT INTO dbo.Exceptions (SourceGuid, ExString)
                        VALUES (@MyGuid, @Myexception)";
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                _ = await conn.ExecuteAsync(sql, new { ex.MyGuid, ex.Myexception });
            }
        }
    }
}
