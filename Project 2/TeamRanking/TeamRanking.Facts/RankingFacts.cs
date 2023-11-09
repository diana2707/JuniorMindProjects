using Xunit;
using static TeamRanking.Ranking;

namespace TeamRanking
{
    public class RankingFacts
    {
        [Fact]
        public void GetPositionOfTeamWhenTeamExists()
        {
            Team team = new Team("Manchester", 13);
            Ranking championship = new ();

            championship.AddTeam(team);

            Assert.Equal(1, championship.PositionOf(new Team("Manchester", 13)));
        }

        [Fact]
        public void GetPositionOfTeamWhenTeamDoNotExists()
        {
            Ranking championship = new ();
            Team team = new Team("Manchester", 13);

            championship.AddTeam(team);

            Assert.Equal(-1, championship.PositionOf(new Team("Bayern Munchen")));
        }

        [Fact]
        public void GetTeamFromPositionWhenPositionExists()
        {
            Ranking championship = new ();
            Team team = new Team("Manchester", 13);

            championship.AddTeam(team);
            Assert.NotNull(championship.FromPosition(1));
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
