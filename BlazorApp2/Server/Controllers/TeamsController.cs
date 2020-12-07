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
        [HttpGet("{myguid:guid}")]
        public async Task<IEnumerable<TeamModel>> TeamsAsync(Guid myguid)
        {
            var sql = "Proc_GetClosestTeamsByRugger";
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                var xx = await conn.QueryAsync<TeamModel, CityInfo, TeamModel>(sql
                    , (teamModel, cityInfo) =>
                     {
                         teamModel.City = cityInfo;
                         return teamModel;
                     }, splitOn: "CityId"
                    , param: new { myguid }
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
            dynamicParameters.Add("CityId", model.TeamCityId);
            dynamicParameters.Add("Owner", model.RuggerOwner);
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
