using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.SqlServer.Types;
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
    public class MyProfileController : Controller
    {
        private SqlConnectionStringBuilder _cstring;

        public MyProfileController(IConfiguration configuration)
        {
            _cstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        [HttpGet("{key}/{lat:double?}/{lng:double?}")]
        public async Task<MyInfo> MyProfileAsync(string key, double lat, double lng)
        {
            var geo = SqlGeography.Point(lat, lng, 4326);
            using (var cnn = new SqlConnection(_cstring.ConnectionString))
            {
              
                var sql = "Proc_GetMyInfo";
                var y = await cnn.QueryAsync<MyInfo, TeamModel, CityInfo, MyInfo>(sql, (info, team, city) =>
                {
                    info.MyTeam = team ?? new TeamModel();
                    info.MyTeam.City = city ?? new CityInfo();
                    return info;
                }, splitOn: "TeamId, CityId", param: new { key }, commandType: CommandType.StoredProcedure);
                var myinfo =  y.FirstOrDefault() ?? new MyInfo();


                sql = "Proc_GetClosestCity";
                myinfo.ClosestCity = await cnn.QueryFirstAsync<CityInfo>(sql, new { geo, key }
                    , commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                return myinfo;
            }
        }
        [HttpPut("[controller]/[action]")]
        public async Task UpdateProfileAsync(UpdateRuggerArgs args)
        {
            using (var conn = new SqlConnection(_cstring.ConnectionString))
            {
                var sql = "Proc_UpsertName";
                await conn.ExecuteAsync(sql, new { Key = args.Key, name = args.MyName, bio = args.Bio }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
