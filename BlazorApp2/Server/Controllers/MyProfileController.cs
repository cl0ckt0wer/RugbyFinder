using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        private IActionDescriptorCollectionProvider _actionDiscriptorCollectionProvider;

        public MyProfileController(IConfiguration configuration, IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _cstring = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
            _actionDiscriptorCollectionProvider = actionDescriptorCollectionProvider;
        }
        [HttpGet("{key}/{lat:double?}/{lng:double?}")]
        public async Task<MyInfo> MyProfileAsync(string key, double? lat, double? lng)
        {
            var routes = _actionDiscriptorCollectionProvider.ActionDescriptors.Items.Select(x => new {
                Action = x.RouteValues["Action"],
                Controller = x.RouteValues["Controller"],
                Name = x.AttributeRouteInfo.Name,
                Template = x.AttributeRouteInfo.Template
            }).ToList();

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

                if(lat != null && lng != null)
                {
                    var geo = SqlGeography.Point(lat.Value, lng.Value, 4326);
                    sql = "Proc_GetClosestCity";
                    myinfo.ClosestCity = await cnn.QueryFirstAsync<CityInfo>(sql, new { geo, key }
                        , commandType: CommandType.StoredProcedure).ConfigureAwait(false);
                   
                }
                return myinfo;
            }
        }
        [HttpPut("[action]")]
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
