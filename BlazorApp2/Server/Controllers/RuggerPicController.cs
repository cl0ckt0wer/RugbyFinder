using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorApp2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuggerPicController : Controller
    {
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;
        private ILogger<RuggersController> _logger;

        public RuggerPicController(ILogger<RuggersController> logger, IConfiguration configuration)
        {
            _logger = logger;
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        [HttpPost]
        public async Task<IActionResult> PostRuggerPicAsync(RuggerPic pic)
        {
            var sql = "Proc_UpsertRuggerPic";
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                await conn.ExecuteAsync(sql, new { key = pic.RuggerKey, b = pic.Pic }, commandType: CommandType.StoredProcedure);
            }
            return Accepted();
        }
    }
}
