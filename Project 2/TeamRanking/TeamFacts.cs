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
    }
}