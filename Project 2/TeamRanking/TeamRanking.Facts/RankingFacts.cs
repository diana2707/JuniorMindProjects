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

            Assert.Equal(1, championship.PositionOf(team));
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
            Assert.Equal(team, championship.FromPosition(1));
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

        [Fact]
        public void UpdateRankingWithNewMatch()
        {
            Team team1 = new ("Barcelona", 10);
            Team team2 = new ("Real Madrid", 7);
            Team team3 = new ("Manchester", 13);
            Team team4 = new ("Liverpool", 5);

            Ranking championship = new ();
            Team[] teams = { team1, team2, team3, team4 };

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Match newMatch = new (team1, team2, Result.WinTeam1);

            championship.UpdateRanking(newMatch);

            Assert.Equal(1, championship.PositionOf(team1));
            Assert.Equal(2, championship.PositionOf(team3));
            Assert.Equal(3, championship.PositionOf(team2));
            Assert.Equal(4, championship.PositionOf(team4));
        }
    }
}
