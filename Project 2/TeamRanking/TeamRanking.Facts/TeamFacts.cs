using Xunit;
using static TeamRanking.Team;

namespace TeamRanking
{
    public class TeamFacts
    {
        [Fact]
        public void AddPointsWhenNoPointsAreSet()
        {
            Team team = new ("Steaua");
            team.AddPoints(6);

            Team result = new ("Steaua", 6);

            Assert.Equivalent(team, result);
        }

        [Fact]
        public void AddPointsWhenPointsAreSet()
        {
            Team team = new ("Steaua", 7);
            team.AddPoints(6);

            Team result = new ("Steaua", 13);

            Assert.Equivalent(team, result);
        }

        [Fact]
        public void ComparePointsWhenPointsAreLesser()
        {
            Team team1 = new ("Rapid", 10);
            Team team2 = new ("Dinamo", 12);
            Assert.True(team1.PointsAreLess(team2));
        }

        [Fact]
        public void ComparePointsWhenPointsAreGreater()
        {
            Team team1 = new ("Rapid", 10);
            Team team2 = new ("Dinamo", 5);
            Assert.False(team1.PointsAreLess(team2));
        }

        [Fact]
        public void ComparePointsWhenPointsAreEqual()
        {
            Team team1 = new ("Rapid", 10);
            Team team2 = new ("Dinamo", 10);
            Assert.False(team1.PointsAreLess(team2));
        }
    }
}