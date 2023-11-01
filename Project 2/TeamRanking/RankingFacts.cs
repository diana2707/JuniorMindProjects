using Xunit;
using static TeamRanking.Ranking;

namespace TeamRanking
{
    public class RankingFacts
    {
        [Fact]
        public void AddTeams()
        {
            Team[] teams = { new Team("Steaua"), new Team("Rapid"), new Team("Dinamo") };
            Ranking championship = new (teams, teams.Length);

            Team teamToAdd = new ("Farul");

            championship.AddTeam(teamToAdd);

            Team[] teamsResult = { new Team("Steaua"), new Team("Rapid"), new Team("Dinamo"), new Team("Farul") };
            Ranking championshipResult = new (teamsResult, teamsResult.Length);

            Assert.Equivalent(championshipResult, championship);
        }

        [Fact]
        public void SortTeamsInDescendingOrder()
        {
            Team[] teams = { new Team("Barcelorna", 10), new Team("Real Madrid", 7), new Team("Manchester", 13), new Team("Liverpool", 5) };
            Ranking championship = new (teams, teams.Length);

            championship.SortTeams();

            Team[] teamsResult = { new Team("Manchester", 13), new Team("Barcelorna", 10), new Team("Real Madrid", 7), new Team("Liverpool", 5) };
            Ranking championshipResult = new (teamsResult, teamsResult.Length);

            Assert.Equivalent(championshipResult, championship);
        }

        [Fact]
        public void GetPositionOfTeamWhenTeamExists()
        {
            Team[] teams = { new Team("Manchester", 13), new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Liverpool", 5) };
            Ranking championship = new (teams, teams.Length);

            Assert.Equal(2, championship.GetPositionOfTeam("Barcelona"));
        }

        [Fact]
        public void GetPositionOfTeamWhenTeamDoNotExists()
        {
            Team[] teams = { new Team("Manchester", 13), new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Liverpool", 5) };
            Ranking championship = new (teams, teams.Length);

            Assert.Equal(-1, championship.GetPositionOfTeam("Bayern Munchen"));
        }

        [Fact]
        public void GetTeamFromPositionWhenPositionExists()
        {
            Team[] teams = { new Team("Manchester", 13), new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Liverpool", 5) };
            Ranking championship = new (teams, teams.Length);

            Assert.Equal("Real Madrid", championship.GetTeamFromPosition(3));
        }

        [Fact]
        public void GetTeamFromPositionWhenPositionDoNotExist()
        {
            Team[] teams = { new Team("Manchester", 13), new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Liverpool", 5) };
            Ranking championship = new (teams, teams.Length);

            Assert.Equal("N/A", championship.GetTeamFromPosition(6));
        }
    }
}
