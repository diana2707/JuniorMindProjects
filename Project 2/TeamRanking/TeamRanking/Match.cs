using System;

namespace TeamRanking
{
    class Match
    {
        readonly Team team1;
        readonly Team team2;
        readonly int scoreTeam1;
        readonly int scoreTeam2;

        public Match(Team team1, Team team2, int scoreTeam1, int scoreTeam2)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.scoreTeam1 = scoreTeam1;
            this.scoreTeam2 = scoreTeam2;
        }

        public void UpdateScore()
        {
            if (scoreTeam1 > scoreTeam2)
            {
                team1.AddPoints(3);
            }
            else if (scoreTeam2 > scoreTeam1)
            {
                team2.AddPoints(3);
            }
            else
            {
                team1.AddPoints(1);
                team2.AddPoints(1);
            }
        }
    }
}
