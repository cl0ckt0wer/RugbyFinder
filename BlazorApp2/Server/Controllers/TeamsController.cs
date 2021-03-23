using BlazorApp2.Shared;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
    public class TeamsController : Controller
    {
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public TeamsController(IConfiguration configuration)
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
        [HttpGet("{key}")]
        public async Task<IEnumerable<TeamModel>> TeamsAsync(string key)
        {
            key = key.Replace("%2f", "%2F").Replace("%2F", "/");

            var sql = "Proc_GetClosestTeamsByRugger";
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                var xx = await conn.QueryAsync<TeamModel, CityInfo, TeamModel>(sql
                    , (teamModel, cityInfo) =>
                     {
                         teamModel.City = cityInfo;
                         return teamModel;
                     }, splitOn: "CityId"
                    , param: new { key }
                    , commandType: CommandType.StoredProcedure);
                return xx;
            }
        }
       
        [HttpPost]
        public async Task<IActionResult> PostTeamsAsync(TeamPostModel model)
        {
          
            var sql = "Proc_UpsertTeam";
            var dynamicParameters = new DynamicParameters();
            dynamicParameters.Add("Id", model.TeamId);
            dynamicParameters.Add("Name", model.TeamName);
            dynamicParameters.Add("Bio", model.TeamBio);
            dynamicParameters.Add("CityId", model.City.CityId);
            dynamicParameters.Add("Owner", model.RuggerOwnerKey);
            dynamicParameters.Add("Teampic", model.TeamPic);
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                var y = await conn.ExecuteAsync(sql, dynamicParameters, commandType: CommandType.StoredProcedure);
                if (y > 0)
                {
                    return Accepted();
                }

            }
            return Ok();

        }

    }
}
