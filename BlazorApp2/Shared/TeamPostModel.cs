using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorApp2.Shared
{
    public class TeamPostModel : TeamModel
    {
        public string RuggerOwnerKey { get; set; } = "";
        public TeamPostModel()
        {

        }
        public TeamPostModel(TeamModel teamModel)
        {
            base.City = teamModel.City;
            base.TeamOwner = teamModel.TeamOwner;
            base.TeamBio = teamModel.TeamBio;
            base.TeamName = teamModel.TeamName;
            base.TeamPic = teamModel.TeamPic;
            base.TeamId = teamModel.TeamId;
        }
    }
}
