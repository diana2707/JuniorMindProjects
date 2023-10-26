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
        public void GetTeamPointsMethodShouldReturn0()
        {
            Team team = new ("Steaua");
            Assert.Equal(0, team.GetTeamPoints());
        }
    }
}