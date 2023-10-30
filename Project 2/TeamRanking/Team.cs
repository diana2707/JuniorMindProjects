using System;

namespace TeamRanking
{
    class Team
    {
        readonly string name;
        private int points;

        public Team(string teamName, int teamPoints = 0)
        {
            this.name = teamName;
            this.points = teamPoints;
        }

        public string GetTeamName()
        {
            return name;
        }

        public int AddTeamPoints(int teamPoints)
        {
            return points += teamPoints;
        }

        public bool ComparePoints(Team otherTeam)
        {
            return this.points < otherTeam.points;
        }
    }
}
