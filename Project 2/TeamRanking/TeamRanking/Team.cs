using System;

namespace TeamRanking
{
    class Team
    {
        private readonly string name;
        private int points;

        public Team(string teamName, int teamPoints = 0)
        {
            this.name = teamName;
            this.points = teamPoints;
        }

        public void AddPoints(int teamPoints)
        {
            points += teamPoints;
        }

        public bool PointsAreLess(Team otherTeam)
        {
            return this.points < otherTeam.points;
        }
    }
}
