using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.SqlServer.Types;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlazorApp2.Server.Controllers
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
        public IActionResult Index()
        {
            return Ok();
        }
        [HttpPost]
        public async Task<IEnumerable<ClosestRuggers>> RuggersAsync(CityInfoArgs newcityinfoargs)
        {
            var g = SqlGeography.Point(newcityinfoargs.Lat, newcityinfoargs.Lng, 4326);

            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                var sql = "Proc_GetClosestRuggers";
                var ret = await conn.QueryAsync<ClosestRuggers>(sql, new { geo = g, ui = newcityinfoargs.MyGuid }, commandType: CommandType.StoredProcedure);
                return ret;
            }

        }
    }
}
