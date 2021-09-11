using RugbyFinder.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Types;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Threading.Tasks;

namespace RugbyFinder.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RuggersController : Controller
    {
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;
        private ILogger<RuggersController> _logger;

        public RuggersController(ILogger<RuggersController> logger, IConfiguration configuration)
        {
            _logger = logger;
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
       
        [HttpGet("{key}/{lat:double}/{lng:double}")]
        public async Task<IEnumerable<ClosestRuggers>> RuggersAsync(string key, double lat, double lng)
        {
            key = key.Replace("%2f", "%2F").Replace("%2F", "/");

            var geo = SqlGeography.Point(lat, lng, 4326);

            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                var sql = "Proc_GetClosestRuggers";
                var ret = await conn.QueryAsync<ClosestRuggers>(sql, new { geo, key }, commandType: CommandType.StoredProcedure);
                return ret;
            }

        }
    }
}
