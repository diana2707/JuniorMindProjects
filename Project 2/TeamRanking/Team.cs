using System;

namespace TeamRanking
{
    class Team
    {
        readonly string name;

        public Team(string teamName)
        {
            name = teamName;
        }

        public string GetTeamName()
        {
            return name;
        }
    }
}
