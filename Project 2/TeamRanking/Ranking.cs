using System;

namespace TeamRanking
{
    class Ranking
    {
        private Team[] teams;
        private int teamsNumber;

        public Ranking(Team[] teams, int teamsNumber)
        {
            this.teams = teams;
            this.teamsNumber = teamsNumber;

            _ = new Team[teamsNumber];
        }

        public void AddTeam(Team team)
        {
            teamsNumber++;
            Array.Resize(ref teams, teamsNumber);
            teams[teamsNumber - 1] = team;
        }

        public void SortTeams()
        {
            for (int i = 0; i < teamsNumber; i++)
            {
                for (int j = 0; j < teamsNumber - 1; j++)
                {
                    if (teams[j].PointsAreLess(teams[j + 1]))
                    {
                        Team temp = teams[j];
                        teams[j] = teams[j + 1];
                        teams[j + 1] = temp;
                    }
                }
            }
        }

        public int GetPositionOfTeam(string teamName)
        {
            for (int i = 0; i < teamsNumber; i++)
            {
                if (teams[i].CompareName(teamName))
                {
                    return i + 1;
                }
            }

            return -1;
        }

        public string GetTeamFromPosition(int position)
        {
            if (position > teamsNumber - 1)
            {
                return "N/A";
            }

            return teams[position - 1].GetTeamName();
        }

        public bool AreEqual(Ranking championship)
        {
            if (championship.teamsNumber != this.teamsNumber)
            {
                return false;
            }

            for (int i = 0; i < teamsNumber; i++)
            {
                if (!this.teams[i].IsEqual(championship.teams[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
