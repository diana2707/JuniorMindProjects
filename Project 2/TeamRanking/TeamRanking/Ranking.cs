using System;

namespace TeamRanking
{
    class Ranking
    {
        private Team[] teams;

        public Ranking()
        {
            this.teams = new Team[0];
        }

        public void AddTeam(Team team)
        {
            Array.Resize(ref teams, teams.Length + 1);
            teams[^1] = team;
        }

        public int PositionOf(Team team)
        {
            for (int i = 0; i < teams.Length; i++)
            {
                if (teams[i].Equals(team))
                {
                    return i + 1;
                }
            }

            return -1;
        }

        public Team? FromPosition(int position)
        {
            if (position < 1 || position > teams.Length)
            {
                return null;
            }

            return teams[position - 1];
        }

        public void UpdateRanking(Match match)
        {
            match.UpdateScore();
            SortTeams();
        }

        void SortTeams()
        {
            for (int i = 0; i < teams.Length; i++)
            {
                for (int j = 0; j < teams.Length - 1; j++)
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
    }
}
