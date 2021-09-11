using RugbyFinder.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyFinder.Server.Controllers
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
        [HttpPost]
        public async Task PostExeceptionAsync(ExceptionArgs ex)
        {
            var lookup = @"SELECT ID FROM dbo.KeyGuid WHERE [Key] = @key";
            var sql = @"INSERT INTO dbo.Exceptions (SourceGuid, ExString)
                        VALUES (@MyGuid, @Myexception)";
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                if (ex.MyGuid == Guid.Empty && !string.IsNullOrEmpty(ex.Key))
                {
                    var x = conn.ExecuteScalar<Guid>(lookup, new { Key = ex.Key });
                    ex.MyGuid = x;
                }
                _ = await conn.ExecuteAsync(sql, new { ex.MyGuid, ex.Myexception });
            }
        }
    }
}
