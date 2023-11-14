using System;

namespace TeamRanking
{
    class Match
    {
        readonly Team homeTeam;
        readonly Team visitingTeam;
        readonly int scoreHomeTeam;
        readonly int scoreVisitingTeam;

        public Match(Team homeTeam, Team visitingTeam, int scoreHomeTeam, int scoreVisitingTeam)
        {
            this.homeTeam = homeTeam;
            this.visitingTeam = visitingTeam;
            this.scoreHomeTeam = scoreHomeTeam;
            this.scoreVisitingTeam = scoreVisitingTeam;
        }

        public void UpdateScore()
        {
            if (scoreHomeTeam > scoreVisitingTeam)
            {
                homeTeam.AddPoints(3);
            }
            else if (scoreVisitingTeam > scoreHomeTeam)
            {
               visitingTeam.AddPoints(3);
            }
            else
            {
                homeTeam.AddPoints(1);
                visitingTeam.AddPoints(1);
            }
        }
    }
}
