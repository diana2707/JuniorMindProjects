using Xunit;
using static TeamRanking.Ranking;

namespace TeamRanking
{
    public class RankingFacts
    {
        [Fact]
        public void SortTeams()
        {
            Team[] teams = { new Team("Steaua"), new Team("Rapid"), new Team("Dinamo") };
            Ranking championship = new (teams, teams.Length);

            Team teamToAdd = new ("Farul");

            championship.AddTeam(teamToAdd);

            Team[] result = { new Team("Steaua"), new Team("Rapid"), new Team("Dinamo"), new Team("Farul") };

            Assert.Equivalent(championship, result);
        }
    }
}
