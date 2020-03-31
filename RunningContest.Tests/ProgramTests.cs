using System;
using Xunit;

namespace RunningContest.Tests
{
    public class ProgramTests
    {
        [Fact]
        public void GenerateGeneralRankingWorkingWorksWithUnsortedData()
        {
            Contestant ion = new Contestant("Ion", "Romania", 9.80);
            Contestant john = new Contestant("John", "Sua", 9.825);
            Contestant zoli = new Contestant("Zoli", "Ungaria", 9.91);
            Contestant michael = new Contestant("Michael ", "Franta", 9.81);
            Contestant vasile = new Contestant("Vasile", "Romania", 9.90);
            Contestant adriano = new Contestant("Adriano ", "Italia", 9.925);

            ContestRanking ser1 = new ContestRanking();
            ContestRanking ser2 = new ContestRanking();
            ser1.Contestants = new Contestant[] { ion, john, zoli };
            ser2.Contestants = new Contestant[] { michael, vasile, adriano };
            Contest contest = new Contest();

            contest.Series = new ContestRanking[] { ser1, ser2 };
            ContestRanking expectedGeneralRating = new ContestRanking();

            expectedGeneralRating.Contestants = new Contestant[] { ion, michael, john, vasile, zoli, adriano };

            Program.GenerateGeneralRanking(ref contest);

            Assert.Equal<Contestant>(contest.GeneralRanking.Contestants, expectedGeneralRating.Contestants);
        }

        [Fact]
        public void GenerateGeneralRankingWorksOnOneSeries()
        {
            Contestant ion = new Contestant("Ion", "Romania", 9.80);
            Contestant john = new Contestant("John", "Sua", 9.825);
            Contestant zoli = new Contestant("Zoli", "Ungaria", 9.91);
            ContestRanking ser1 = new ContestRanking();
            ser1.Contestants = new Contestant[] { ion, john, zoli };
            Contest contest = new Contest
            {
                Series = new ContestRanking[] { ser1}
            };
            ContestRanking expectedGeneralRating = new ContestRanking();

            expectedGeneralRating.Contestants = new Contestant[] { ion, john, zoli };

            Program.GenerateGeneralRanking(ref contest);

            Assert.Equal<Contestant>(contest.GeneralRanking.Contestants, expectedGeneralRating.Contestants);
        }

        [Fact]
        public void GenerateGeneralRankingWorksOnOneContestantPerSeries()
        {
            Contestant ion = new Contestant("Ion", "Romania", 9.80);
            Contestant john = new Contestant("John", "Sua", 9.825);
            Contestant zoli = new Contestant("Zoli", "Ungaria", 9.91);
            ContestRanking ser1 = new ContestRanking();
            ContestRanking ser2 = new ContestRanking();
            ContestRanking ser3 = new ContestRanking();
            ser1.Contestants = new Contestant[] { ion };
            ser2.Contestants = new Contestant[] { john };
            ser3.Contestants = new Contestant[] { zoli };
            Contest contest = new Contest
            {
                Series = new ContestRanking[] { ser1, ser2, ser3 }
            };
            ContestRanking expectedGeneralRating = new ContestRanking();

            expectedGeneralRating.Contestants = new Contestant[] { ion, john, zoli };

            Program.GenerateGeneralRanking(ref contest);

            Assert.Equal<Contestant>(contest.GeneralRanking.Contestants, expectedGeneralRating.Contestants);
        }
    }
}
