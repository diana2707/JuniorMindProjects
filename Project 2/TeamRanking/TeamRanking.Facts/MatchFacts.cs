using Xunit;
using static TeamRanking.Match;

namespace TeamRanking
{
    public class MatchFacts
    {
        [Fact]
        public void UpdateRankingWhenTeam1Wins()
        {
            Team barcelona = new ("Barcelona", 5);
            Team realMadrid = new ("Real Madrid", 6);
            Team manchester = new ("Manchester", 7);
            Team liverpool = new ("Liverpool", 9);

            Ranking championship = new ();

            championship.AddTeam(barcelona);
            championship.AddTeam(realMadrid);
            championship.AddTeam(manchester);
            championship.AddTeam(liverpool);

            Match newMatch = new (barcelona, realMadrid, 2, 1);

            championship.UpdateRanking(newMatch);

            Assert.Equal(1, championship.PositionOf(liverpool));
            Assert.Equal(2, championship.PositionOf(barcelona));
            Assert.Equal(3, championship.PositionOf(manchester));
            Assert.Equal(4, championship.PositionOf(realMadrid));
        }

        [Fact]
        public void UpdateRankingWhenTeam2Wins()
        {
            Team barcelona = new ("Barcelona", 5);
            Team realMadrid = new ("Real Madrid", 6);
            Team manchester = new ("Manchester", 7);
            Team liverpool = new ("Liverpool", 9);

            Ranking championship = new ();

            championship.AddTeam(barcelona);
            championship.AddTeam(realMadrid);
            championship.AddTeam(manchester);
            championship.AddTeam(liverpool);

            Match newMatch = new (barcelona, realMadrid, 0, 3);

            championship.UpdateRanking(newMatch);

            Assert.Equal(1, championship.PositionOf(realMadrid));
            Assert.Equal(2, championship.PositionOf(liverpool));
            Assert.Equal(3, championship.PositionOf(manchester));
            Assert.Equal(4, championship.PositionOf(barcelona));
        }

        [Fact]
        public void UpdateRankingWhenIsTie()
        {
            Team barcelona = new ("Barcelona", 5);
            Team realMadrid = new ("Real Madrid", 6);
            Team manchester = new ("Manchester", 7);
            Team liverpool = new ("Liverpool", 9);

            Ranking championship = new ();

            championship.AddTeam(barcelona);
            championship.AddTeam(realMadrid);
            championship.AddTeam(manchester);
            championship.AddTeam(liverpool);

            Match newMatch = new (barcelona, realMadrid, 1, 1);

            championship.UpdateRanking(newMatch);

            Assert.Equal(1, championship.PositionOf(liverpool));
            Assert.Equal(2, championship.PositionOf(realMadrid));
            Assert.Equal(3, championship.PositionOf(manchester));
            Assert.Equal(4, championship.PositionOf(barcelona));
        }
    }
}