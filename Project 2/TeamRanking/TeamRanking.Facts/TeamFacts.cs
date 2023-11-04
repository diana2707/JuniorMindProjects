using Xunit;
using static TeamRanking.Team;

namespace TeamRanking
{
    public class TeamFacts
    {
        [Fact]
        public void GetTeamNameMethodShouldReturnValidString()
        {
            Team team = new ("Steaua");
            Assert.Equal("Steaua", team.GetTeamName());
        }

        [Fact]
        public void AddPointsWhenNoPointsAreSet()
        {
            Team team = new ("Steaua");
            Assert.Equal(6, team.AddTeamPoints(6));
        }

        [Fact]
        public void AddPointsWhenPointsAreSet()
        {
            Team team = new ("Steaua", 7);
            Assert.Equal(10, team.AddTeamPoints(3));
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

        [Fact]
        public void CompareNameOfTeamWhenTheyMatch()
        {
            Team team = new ("Liverpool");
            Assert.True(team.CompareName("Liverpool"));
        }

        [Fact]
        public void CompareNameOfTeamWhenTheyDoNotMatch()
        {
            Team team = new ("Manchester");
            Assert.False(team.CompareName("Liverpool"));
        }

        [Fact]
        public void IsEqualWhenTeamsAreTheSame()
        {
            Team team1 = new ("Manchester", 7);
            Team team2 = new ("Manchester", 7);
            Assert.True(team1.IsEqual(team2));
        }

        [Fact]
        public void IsEqualWhenTeamsNameIsNotTheSame()
        {
            Team team1 = new ("Manchester", 7);
            Team team2 = new ("Real Madrid", 7);
            Assert.False(team1.IsEqual(team2));
        }

        [Fact]
        public void IsEqualWhenTeamsPointsIsNotTheSame()
        {
            Team team1 = new ("Manchester", 7);
            Team team2 = new ("Manchester", 10);
            Assert.False(team1.IsEqual(team2));
        }

        [Fact]
        public void IsEqualWhenTeamsAreDifferent()
        {
            Team team1 = new ("Manchester", 3);
            Team team2 = new ("Real Madrid", 11);
            Assert.False(team1.IsEqual(team2));
        }
    }
}