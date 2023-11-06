using Xunit;
using static TeamRanking.Ranking;

namespace TeamRanking
{
    public class RankingFacts
    {
        [Fact]
        public void AddNewTeam()
        {
            Ranking championship = new ();
            Team[] teams = { new Team("Steaua"), new Team("Rapid"), new Team("Dinamo") };

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Team teamToAdd = new ("Farul");

            championship.AddTeam(teamToAdd);

            Ranking championshipResult = new ();
            Team[] teamsResult = { new Team("Steaua"), new Team("Rapid"), new Team("Dinamo"), new Team("Farul") };

            for (int i = 0; i < teamsResult.Length; i++)
            {
                championshipResult.AddTeam(teamsResult[i]);
            }

            Assert.Equal(championshipResult, championship);
        }

        [Fact]
        public void UpdateRankingWithNewMatch()
        {
            Team team1 = new ("Barcelorna", 10);
            Team team2 = new ("Real Madrid", 7);

            Ranking championship = new ();
            Team[] teams = { team1, team2, new Team("Manchester", 13), new Team("Liverpool", 5) };

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Match newMatch = new (team1, team2, Result.WinTeam1);

            championship.UpdateRanking(newMatch);

            Team[] teamsResult = { new Team("Barcelorna", 13), new Team("Manchester", 13), new Team("Real Madrid", 7), new Team("Liverpool", 5) };
            Ranking championshipResult = new ();

            for (int i = 0; i < teamsResult.Length; i++)
            {
                championshipResult.AddTeam(teamsResult[i]);
            }

            Assert.Equal(championshipResult, championship);
        }

        [Fact]
        public void GetPositionOfTeamWhenTeamExists()
        {
            Team[] teams = { new Team("Manchester", 13), new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Liverpool", 5) };
            Ranking championship = new ();

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Assert.Equal(1, championship.PositionOf(new Team("Manchester", 13)));
        }

        [Fact]
        public void GetPositionOfTeamWhenTeamDoNotExists()
        {
            Team[] teams = { new Team("Manchester", 13), new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Liverpool", 5) };
            Ranking championship = new ();

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Assert.Equal(-1, championship.PositionOf(new Team("Bayern Munchen")));
        }

        [Fact]
        public void GetTeamFromPositionWhenPositionExists()
        {
            Team[] teams = { new Team("Manchester", 13), new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Liverpool", 5) };
            Ranking championship = new ();

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Assert.Equal(new Team("Real Madrid", 7), championship.FromPosition(3));
        }

        [Fact]
        public void GetTeamFromPositionWhenPositionDoNotExist()
        {
            Team[] teams = { new Team("Manchester", 13), new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Liverpool", 5) };
            Ranking championship = new ();

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Assert.Null(championship.FromPosition(6));
        }
    }
}
