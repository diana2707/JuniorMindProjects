using Xunit;
using static TeamRanking.Match;

namespace TeamRanking
{
    public class MatchFacts
    {
        [Fact]
        public void UpdateRankingWhenTeam1Wins()
        {
            Team[] teams = { new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Manchester", 13), new Team("Liverpool", 5) };
            Ranking championship = new (teams, teams.Length);

            Match match = new (teams[0], teams[1], Result.WinTeam1);

            match.UpdateRanking(championship);

            Team[] teamsResult = { new Team("Barcelona", 13), new Team("Manchester", 13), new Team("Real Madrid", 7), new Team("Liverpool", 5) };
            Ranking championshipResult = new (teamsResult, teamsResult.Length);

            Assert.True(championship.AreEqual(championshipResult));
        }

        [Fact]
        public void UpdateRankingWhenTeam2Wins()
        {
            Team[] teams = { new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Manchester", 13), new Team("Liverpool", 5) };
            Ranking championship = new (teams, teams.Length);

            Match match = new (teams[0], teams[1], Result.WinTeam2);

            match.UpdateRanking(championship);

            Team[] teamsResult = { new Team("Manchester", 13), new Team("Barcelona", 10), new Team("Real Madrid", 10), new Team("Liverpool", 5) };
            Ranking championshipResult = new (teamsResult, teamsResult.Length);

            Assert.True(championship.AreEqual(championshipResult));
        }

        [Fact]
        public void UpdateRankingWhenIsTie()
        {
            Team[] teams = { new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Manchester", 13), new Team("Liverpool", 5) };
            Ranking championship = new (teams, teams.Length);

            Match match = new (teams[0], teams[1], Result.Tie);

            match.UpdateRanking(championship);

            Team[] teamsResult = { new Team("Manchester", 13), new Team("Barcelona", 11), new Team("Real Madrid", 8), new Team("Liverpool", 5) };
            Ranking championshipResult = new (teamsResult, teamsResult.Length);

            Assert.True(championship.AreEqual(championshipResult));
        }
    }
}