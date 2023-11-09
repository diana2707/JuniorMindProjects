using Xunit;
using static TeamRanking.Match;

namespace TeamRanking
{
    public class MatchFacts
    {
        /*[Fact]
        public void UpdateScoreWhenTeam1Wins()
        {
            Team team1 = new ("Barcelona", 10);
            Team team2 = new ("Real Madrid", 7);

            Match match = new (team1, team2, Result.WinTeam1);

            match.UpdateScore();

            Assert.Equals(championship, championshipResult);
        }

        [Fact]
        public void UpdateRankingWhenTeam2Wins()
        {
            Ranking championship = new ();
            Team[] teams = { new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Manchester", 13), new Team("Liverpool", 5) };

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Match match = new (teams[0], teams[1], Result.WinTeam2);

            match.UpdateScore(championship);

            Team[] teamsResult = { new Team("Barcelona", 10), new Team("Real Madrid", 10), new Team("Manchester", 13), new Team("Liverpool", 5) };
            Ranking championshipResult = new ();

            Assert.Equivalent(championship, championshipResult);
        }

        [Fact]
        public void UpdateRankingWhenIsTie()
        {
            Ranking championship = new ();
            Team[] teams = { new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Manchester", 13), new Team("Liverpool", 5) };

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Match match = new (teams[0], teams[1], Result.Tie);

            match.UpdateScore(championship);

            Team[] teamsResult = { new Team("Barcelona", 11), new Team("Real Madrid", 8), new Team("Manchester", 13), new Team("Liverpool", 5) };
            Ranking championshipResult = new ();

            Assert.Equivalent(championship, championshipResult);
        }*/

        [Fact]
        public void UpdateRankingWhenTeam1Wins()
        {
            Team team1 = new ("Barcelona", 5);
            Team team2 = new ("Real Madrid", 6);
            Team team3 = new ("Manchester", 7);
            Team team4 = new ("Liverpool", 9);

            Ranking championship = new ();
            Team[] teams = { team1, team2, team3, team4 };

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Match newMatch = new (team1, team2, Result.WinTeam1);

            championship.UpdateRanking(newMatch);

            Assert.Equal(1, championship.PositionOf(team4));
            Assert.Equal(2, championship.PositionOf(team1));
            Assert.Equal(3, championship.PositionOf(team3));
            Assert.Equal(4, championship.PositionOf(team2));
        }

        [Fact]
        public void UpdateRankingWhenTeam2Wins()
        {
            Team team1 = new ("Barcelona", 5);
            Team team2 = new ("Real Madrid", 6);
            Team team3 = new ("Manchester", 7);
            Team team4 = new ("Liverpool", 9);

            Ranking championship = new ();
            Team[] teams = { team1, team2, team3, team4 };

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Match newMatch = new (team1, team2, Result.WinTeam2);

            championship.UpdateRanking(newMatch);

            Assert.Equal(1, championship.PositionOf(team2));
            Assert.Equal(2, championship.PositionOf(team4));
            Assert.Equal(3, championship.PositionOf(team3));
            Assert.Equal(4, championship.PositionOf(team1));
        }

        [Fact]
        public void UpdateRankingWhenIsTie()
        {
            Team team1 = new ("Barcelona", 5);
            Team team2 = new ("Real Madrid", 6);
            Team team3 = new ("Manchester", 7);
            Team team4 = new ("Liverpool", 9);

            Ranking championship = new ();
            Team[] teams = { team1, team2, team3, team4 };

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Match newMatch = new (team1, team2, Result.Tie);

            championship.UpdateRanking(newMatch);

            Assert.Equal(1, championship.PositionOf(team4));
            Assert.Equal(2, championship.PositionOf(team2));
            Assert.Equal(3, championship.PositionOf(team3));
            Assert.Equal(4, championship.PositionOf(team1));
        }
    }
}