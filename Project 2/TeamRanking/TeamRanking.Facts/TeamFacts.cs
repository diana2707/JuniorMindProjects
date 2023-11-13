using Xunit;
using static TeamRanking.Team;

namespace TeamRanking
{
    public class TeamFacts
    {
        [Fact]
        public void AddPointsWhenNoPointsAreSet()
        {
            Team steaua = new ("Steaua");
            steaua.AddPoints(6);

            Team result = new ("Steaua", 6);

            Assert.Equivalent(steaua, result);
        }

        [Fact]
        public void AddPointsWhenPointsAreSet()
        {
            Team steaua = new ("Steaua", 7);
            steaua.AddPoints(6);

            Team result = new ("Steaua", 13);

            Assert.Equivalent(steaua, result);
        }

        [Fact]
        public void ComparePointsWhenPointsAreLesser()
        {
            Team rapid = new ("Rapid", 10);
            Team dinamo = new ("Dinamo", 12);
            Assert.True(rapid.PointsAreLess(dinamo));
        }

        [Fact]
        public void ComparePointsWhenPointsAreGreater()
        {
            Team rapid = new ("Rapid", 10);
            Team dinamo = new ("Dinamo", 5);
            Assert.False(rapid.PointsAreLess(dinamo));
        }

        [Fact]
        public void ComparePointsWhenPointsAreEqual()
        {
            Team rapid = new ("Rapid", 10);
            Team dinamo = new ("Dinamo", 10);
            Assert.False(rapid.PointsAreLess(dinamo));
        }
    }
}