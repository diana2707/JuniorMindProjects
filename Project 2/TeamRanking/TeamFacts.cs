using Xunit;
using static TeamRanking.Team;

namespace TeamRanking
{
    public class TeamFacts
    {
        [Fact]
        public void GetTeamName()
        {
            Team team = new ("Steaua");
            Assert.Equal("Steaua", team.GetTeamName());
        }
    }
}