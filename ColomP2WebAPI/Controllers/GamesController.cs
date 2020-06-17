using ColomP2WebAPI.Models;
using Dapper;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ColomP2WebAPI.Controllers
{
    [Authorize]
    [RoutePrefix("api/Games")]
    public class GamesController : ApiController
    {
        [HttpGet]
        [Route("GetGamesInfo")]

        public List<GamesModel> GetGamesInfo()
        {
            string authenticatedAspNetUserId = RequestContext.Principal.Identity.GetUserId();
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"SELECT PlayerName,Age,HourGame,Score FROM dbo.Games ORDER BY Score ASC";

                var games = cnn.Query<GamesModel>(sql).ToList();

                return games;
            }
        }


        [HttpPost]
        [Route("InsertGame")]
        public bool InsertGame(GamesModel games)
        {
            using (IDbConnection cnn = new ApplicationDbContext().Database.Connection)
            {
                string sql = $"INSERT INTO dbo.Games(PlayerName,Age,HourGame,Score) VALUES('{games.PlayerName}','{games.Age}','{games.HourGame}','{games.Score}')";

                int rows = cnn.Execute(sql);
                if (rows != 1)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }


        }
    }
}
