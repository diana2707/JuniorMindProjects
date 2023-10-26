using System;

namespace TeamRanking
{
    class Team
    {
        readonly string name;
        readonly int points;

        public Team(string teamName, int teamPoints = 0)
        {
            name = teamName;
            points = teamPoints;
        }

        public string GetTeamName()
        {
            return name;
        }

        public int GetTeamPoints()
        {
            return points;
        }
    }
}
