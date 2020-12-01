﻿using BlazorApp2.Shared;
using Dapper;
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
    public class TeamProfileController : Controller
    {
        private SqlConnectionStringBuilder sqlConnectionStringBuilder;

        public TeamProfileController(IConfiguration configuration)
        {
            sqlConnectionStringBuilder = new SqlConnectionStringBuilder(configuration.GetConnectionString("MessagingDatabase"));
        }
        [HttpGet]
        [Route("{id:guid}")]
        public async Task<TeamModel> GetTeamProfileAsync(Guid id)
        {
            var sql = "Get_TeamProfileInfo";
            using (var conn = new SqlConnection(sqlConnectionStringBuilder.ConnectionString))
            {
                var test = await conn.QueryAsync<TeamModel, CityInfo, TeamModel>(sql
                             , (teamModel, cityInfo) =>
                             {
                                 teamModel.City = cityInfo;
                                 return teamModel;
                             }, splitOn: "CityId"
                             , param: new { teamid = id }
                             , commandType: CommandType.StoredProcedure);
                return test.FirstOrDefault();
            }

        }
    }
}