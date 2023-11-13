using Xunit;
using static TeamRanking.Ranking;

namespace TeamRanking
{
    public class RankingFacts
    {
        [Fact]
        public void GetPositionOfTeamWhenTeamExists()
        {
            Team manchester = new Team("Manchester", 13);
            Ranking championship = new ();

            championship.AddTeam(manchester);

            Assert.Equal(1, championship.PositionOf(manchester));
        }

        [Fact]
        public void GetPositionOfTeamWhenTeamDoNotExists()
        {
            Ranking championship = new ();
            Team manchester = new Team("Manchester", 13);

            championship.AddTeam(manchester);

            Assert.Equal(-1, championship.PositionOf(new Team("Bayern Munchen")));
        }

        [Fact]
        public void GetTeamFromPositionWhenPositionExists()
        {
            Ranking championship = new ();
            Team manchester = new Team("Manchester", 13);

            championship.AddTeam(manchester);
            Assert.Equal(manchester, championship.FromPosition(1));
        }

        [Fact]
        public void GetTeamFromPositionWhenPositionDoNotExist()
        {
            Team manchester = new Team("Manchester", 13);
            Team barcelona = new Team("Barcelona", 10);
            Team realMadrid = new Team("Real Madrid", 7);
            Team liverpool = new Team("Liverpool", 5);

            Ranking championship = new ();

            championship.AddTeam(manchester);
            championship.AddTeam(barcelona);
            championship.AddTeam(realMadrid);
            championship.AddTeam(liverpool);

            Assert.Null(championship.FromPosition(6));
        }

        [Fact]
        public void UpdateRankingWithNewMatch()
        {
            Team barcelona = new ("Barcelona", 10);
            Team realMadrid = new ("Real Madrid", 7);
            Team manchester = new ("Manchester", 13);
            Team liverpool = new ("Liverpool", 5);

            Ranking championship = new ();
            championship.AddTeam(barcelona);
            championship.AddTeam(realMadrid);
            championship.AddTeam(manchester);
            championship.AddTeam(liverpool);

            Match newMatch = new (barcelona, realMadrid, 1, 0);

            championship.UpdateRanking(newMatch);

            Assert.Equal(1, championship.PositionOf(barcelona));
            Assert.Equal(2, championship.PositionOf(manchester));
            Assert.Equal(3, championship.PositionOf(realMadrid));
            Assert.Equal(4, championship.PositionOf(liverpool));
        }
    }
}
