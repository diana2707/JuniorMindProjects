using System;

namespace TeamRanking
{
    enum Result
    {
        WinTeam1,
        WinTeam2,
        Tie
    }

    class Match
    {
        readonly Team team1;
        readonly Team team2;
        readonly Result result;

        public Match(Team team1, Team team2, Result result)
        {
            this.team1 = team1;
            this.team2 = team2;
            this.result = result;
        }

        public void UpdateRanking(Ranking championship)
        {
            if (this.result == Result.WinTeam1)
            {
                team1.AddTeamPoints(3);
            }
            else if (this.result == Result.WinTeam2)
            {
                team2.AddTeamPoints(3);
            }
            else
            {
                team1.AddTeamPoints(1);
                team2.AddTeamPoints(1);
            }

            championship.SortTeams();
        }
    }
}
