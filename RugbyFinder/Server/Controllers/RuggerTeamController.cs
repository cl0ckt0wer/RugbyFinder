using RugbyFinder.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RugbyFinder.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuggerTeamController : Controller
    {
        private ILogger<RuggersController> _logger;
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public RuggerTeamController(ILogger<RuggersController> logger, IConfiguration configuration)
        {
            _logger = logger;
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostRuggerTeamAsync(RuggerTeamArgs args)
        {
            var sql = "Proc_SetRuggerTeam";
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                var x = await conn.ExecuteAsync(sql, new { Key = args.Key, TeamId = args.TeamId }, commandType: CommandType.StoredProcedure);

                return Accepted();
            }

        }
    }
}
