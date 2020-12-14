using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Types;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace BlazorApp2.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CityController : Controller
    {
        private SqlConnectionStringBuilder _cstring;

        public CityController(IConfiguration configuration)
        {
            _cstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<CityInfo> PostCityAsync(CityInfoArgs args)
        {
            var g = SqlGeography.Point(args.Lat, args.Lng, 4326);
            string sql = "Proc_GetClosestCity";
            using (var cnn = new SqlConnection(_cstring.ConnectionString))
            {
                var city = await cnn.QueryFirstOrDefaultAsync<CityInfo>(sql, new { geo = g, ui = args.MyGuid }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return city;
            }

        }
        [Route("{myguid:guid}")]
        public async Task<IEnumerable<CityInfo>> GetCityAsync(Guid myguid)
        {
            var sql = "Proc_GetClosestCitiesByRugger";
            using (var conn = new SqlConnection(_cstring.ConnectionString))
            {
                var test = await conn.QueryAsync<CityInfo>(sql, param: new { myguid = myguid }, commandType: CommandType.StoredProcedure);
                return test;
            }
        }
    }
}
