using System;

namespace TeamRanking
{
    class Ranking
    {
        private Team[] teams;
        private int teamsNumber;

        public Ranking(Team[] teams, int teamsNumber)
        {
            this.teams = teams;
            this.teamsNumber = teamsNumber;

            _ = new Team[teamsNumber];
        }

        public void AddTeam(Team team)
        {
            teamsNumber++;
            Array.Resize(ref teams, teamsNumber);
            teams[teamsNumber - 1] = team;
        }
    }
}
