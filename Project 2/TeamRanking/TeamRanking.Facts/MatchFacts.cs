using Xunit;
using static TeamRanking.Match;

namespace TeamRanking
{
    public class MatchFacts
    {
        [Fact]
        public void UpdateRankingWhenTeam1Wins()
        {
            Ranking championship = new ();
            Team[] teams = { new Team("Barcelona", 10), new Team("Real Madrid", 7), new Team("Manchester", 13), new Team("Liverpool", 5) };

            for (int i = 0; i < teams.Length; i++)
            {
                championship.AddTeam(teams[i]);
            }

            Match match = new (teams[0], teams[1], Result.WinTeam1);

            match.UpdateScore(championship);

            Team[] teamsResult = { new Team("Barcelona", 13), new Team("Real Madrid", 7), new Team("Manchester", 13), new Team("Liverpool", 5) };
            Ranking championshipResult = new ();

            Assert.Equivalent(championship, championshipResult);
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
        }
    }
}