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

            Team[] result = { new Team("Steaua"), new Team("Rapid"), new Team("Dinamo"), new Team("Farul") };

            Assert.Equivalent(championship, result);
        }

        [Fact]
        public void SortTeams()
        {
            Team[] teams = { new Team("Barcelorna", 10), new Team("Real Madrid", 7), new Team("Manchester", 13), new Team("Liverpool", 5) };
            Ranking championship = new (teams, teams.Length);

            championship.SortTeams();

            Team[] result = { new Team("Manchester", 13), new Team("Barcelorna", 10), new Team("Real Madrid", 7), new Team("Liverpool", 5) };

            Assert.Equivalent(championship, result);
        }
    }
}
