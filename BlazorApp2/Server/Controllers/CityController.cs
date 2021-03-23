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
     
        [HttpPost]
        [Obsolete("This method is obsolete. Use MyProfile instead.", false)]
        public async Task<CityInfo> PostCityAsync(CityInfoArgs args)
        {
            var g = SqlGeography.Point(args.Lat, args.Lng, 4326);
            string sql = "Proc_GetClosestCity";
            using (var cnn = new SqlConnection(_cstring.ConnectionString))
            {
                var city = await cnn.QueryFirstAsync<CityInfo>(sql, new { geo = g, key = args.Key }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return city;
            }

        }
        [HttpGet("{key}")]
        [HttpGet("[action]/{key}")]
        public async Task<IEnumerable<CityInfo>> GetCityAsync(string key)
        {
            key = key.Replace("%2f", "%2F").Replace("%2F", "/");

            var sql = "Proc_GetClosestCitiesByRugger";
            using (var conn = new SqlConnection(_cstring.ConnectionString))
            {
                var test = await conn.QueryAsync<CityInfo>(sql, param: new { key }, commandType: CommandType.StoredProcedure);
                return test;
            }
        }
    }
}
